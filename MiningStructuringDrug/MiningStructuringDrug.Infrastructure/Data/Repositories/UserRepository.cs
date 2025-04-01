using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetUserById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapUserFromReader(reader);
                        }

                        return null;
                    }
                }
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetUserByUsername", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapUserFromReader(reader);
                        }

                        return null;
                    }
                }
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetUserByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapUserFromReader(reader);
                        }

                        return null;
                    }
                }
            }
        }

        public async Task AddAsync(User entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CreateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", entity.Username);
                    cmd.Parameters.AddWithValue("@Email", entity.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                    cmd.Parameters.AddWithValue("@PasswordSalt", entity.PasswordSalt);
                    cmd.Parameters.AddWithValue("@Role", entity.Role);

                    SqlParameter idOutputParam = new SqlParameter("@Id", SqlDbType.Int);
                    idOutputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idOutputParam);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    entity.Id = (int)idOutputParam.Value; // Get the new ID
                }
            }
        }

        public async Task UpdateAsync(User entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Username", entity.Username);
                    cmd.Parameters.AddWithValue("@Email", entity.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                    cmd.Parameters.AddWithValue("@PasswordSalt", entity.PasswordSalt);
                    cmd.Parameters.AddWithValue("@Role", entity.Role);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private User MapUserFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Username = (string)reader["Username"],
                Email = (string)reader["Email"],
                PasswordHash = (string)reader["PasswordHash"],
                PasswordSalt = (string)reader["PasswordSalt"],
                Role = (string)reader["Role"]
            };
        }
    }
}
