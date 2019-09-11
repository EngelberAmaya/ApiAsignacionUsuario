using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using apiUsuarioCrud.Models;
// NUEVO
using Microsoft.Extensions.Configuration;

namespace apiUsuarioCrud.Data.Repository
{
    public class BaseRepository
    {
        private readonly string _connectionString;
        string connectionString = "Server=tcp:sfnetlab.database.windows.net,1433;Initial Catalog=dbtest;Persist Security Info=False;User ID=testuser;Password=Factory2020;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BaseContext");
        }

        //USUARIO

        // guardar
        public async Task Insert(User user)
        {
            

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_USER_REGUSER", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@NAME", user.Name));
                    cmd.Parameters.Add(new SqlParameter("@LASTNAME", user.LastName));
                    cmd.Parameters.Add(new SqlParameter("@AGE", user.Age));
                    cmd.Parameters.Add(new SqlParameter("@LASTSESSION", user.LastSessionDateTime));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        //Eliminar
        public async Task DeleteById(long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DELETE_USER", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        //modificar
        public async Task UpdateUser(User user,long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_USER", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", user.Id));
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@NAME", user.Name));
                    cmd.Parameters.Add(new SqlParameter("@LASTNAME", user.LastName));
                    cmd.Parameters.Add(new SqlParameter("@AGE", user.Age));
                    cmd.Parameters.Add(new SqlParameter("@LASTSESSION", user.LastSessionDateTime));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }




        // SOFTWARE

        public async Task InsertSoftware(Software soft)
        {


            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SOFTWARE_INSERT", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SOFTWARENAME", soft.SoftwareName));
                   
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task DeleteSoftware(long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DELETE_SOFTWARE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task UpdateSoftware(Software soft, long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_SOFTWARE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", soft.Id));
                    cmd.Parameters.Add(new SqlParameter("@SOFTWARENAME", soft.SoftwareName));
                    
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }



        // HARDWARE

        public async Task InsertHardware(Hardware hard)
        {


            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_HARDWARE_INSERT", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@HARDWARENAME", hard.HardwareName));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task DeleteHardware(long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DELETE_HARDWARE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task UpdateHardware(Hardware hard, long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_HARDWARE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", hard.Id));
                    cmd.Parameters.Add(new SqlParameter("@HARDWARENAME", hard.HardwareName));

                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        // ASIGNACIONES


        // para drecuperar los datos
        private object MapModelo(SqlDataReader reader)
        {
            var model = new
            {
                softwareID = reader["SoftwareID"],
                softwareName = reader["SoftwareName"].ToString(),
                HardwareID = reader["HardwareID"],
                hardwareName = reader["HardwareName"].ToString(),
            };
            return model;

        }

        public async Task<object> GetForIdUser(long Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SelectHS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", Id));
                    var response = new List<object>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapModelo(reader));
                        }
                    }

                    return response;
                }
            }
        }


        /*
        public async Task InsertAssignment(Assignment assignment)
        {


            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_ASSIGNMENT_INSERT", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@USERID", assignment.UserID));
                    cmd.Parameters.Add(new SqlParameter("@HARDWAREID", assignment.HardwareID));
                    cmd.Parameters.Add(new SqlParameter("@SOFTWAREID", assignment.SoftwareID));
                    
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }*/


        public async Task UpdateAssignment(Assignment assign, long id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_ASSIGNMENT", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@ID", assign.Id));
                    cmd.Parameters.Add(new SqlParameter("@USERID", assign.UserID));
                    cmd.Parameters.Add(new SqlParameter("@HARDWAREID", assign.HardwareID));
                    cmd.Parameters.Add(new SqlParameter("@SOFTWAREID", assign.SoftwareID));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        
        public async Task DeleteAssignment(long id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DELETE_ASSIGNMENT", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", id));
                    cmd.Parameters.Add(new SqlParameter("@hardwareID", id));
                    cmd.Parameters.Add(new SqlParameter("@softwareID", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        /*
        public async Task DeleteForIdUser(Assignment assignment,long Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteAss", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", Id));
                    cmd.Parameters.Add(new SqlParameter("@hardwareID", Id));
                    cmd.Parameters.Add(new SqlParameter("@softwareID", Id));
                    var response = new List<object>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapModelo(reader));
                        }
                    }

                   return;
                }
            }
        }*/


    }
}
