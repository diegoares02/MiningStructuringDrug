using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Infrastructure.Data.Repositories
{
    public class DrugIndicationRepository : IDrugIndicationRepository
    {
        private readonly string _connectionString;

        public DrugIndicationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<DrugIndication> GetByIdAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetDrugIndicationById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapDrugIndicationFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<DrugIndication>> GetAllAsync()
        {
            List<DrugIndication> drugIndications = new List<DrugIndication>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllDrugIndications", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            drugIndications.Add(MapDrugIndicationFromReader(reader));
                        }
                    }
                }
            }

            return drugIndications;
        }

        public async Task<DrugIndication?> AddAsync(DrugIndication entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CreateDrugIndication", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DrugName", entity.DrugName);

                    SqlParameter idOutputParam = new SqlParameter("@Id", SqlDbType.Int);
                    idOutputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idOutputParam);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    entity.Id = (int)idOutputParam.Value;

                    await AddRelatedDataAsync(conn, entity);
                }
            }
            return entity;
        }

        public async Task UpdateAsync(DrugIndication entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateDrugIndication", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@DrugName", entity.DrugName);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    await DeleteRelatedDataAsync(conn, entity.Id);
                    await AddRelatedDataAsync(conn, entity);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteDrugIndication", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private DrugIndication MapDrugIndicationFromReader(SqlDataReader reader)
        {
            return new DrugIndication
            {
                Id = (int)reader["Id"],
                DrugName = (string)reader["DrugName"]
            };
        }

        private async Task AddRelatedDataAsync(SqlConnection conn, DrugIndication entity)
        {
            using (SqlCommand cmd = new SqlCommand("sp_CreateIndication", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DrugIndicationId", entity.Id);
                SqlParameter indicationParam = cmd.Parameters.Add("@IndicationText", SqlDbType.NVarChar, -1);
                indicationParam.Value = "";

                foreach (var indication in entity.Indications)
                {
                    indicationParam.Value = indication;
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            using (SqlCommand cmd = new SqlCommand("sp_CreateICD10Code", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DrugIndicationId", entity.Id);
                SqlParameter codeParam = cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 20);
                codeParam.Value = "";

                foreach (var code in entity.IcD10Codes)
                {
                    codeParam.Value = code;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task DeleteRelatedDataAsync(SqlConnection conn, int drugIndicationId)
        {
            using (SqlCommand cmd = new SqlCommand("sp_DeleteIndicationsByDrugIndicationId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DrugIndicationId", drugIndicationId);
                await cmd.ExecuteNonQueryAsync();
            }

            using (SqlCommand cmd = new SqlCommand("sp_DeleteICD10CodesByDrugIndicationId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DrugIndicationId", drugIndicationId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
