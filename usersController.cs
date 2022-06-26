using ArduinoCore5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class usersController
    {
        private readonly IConfiguration _configuration;

        public usersController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select username,user_password,user_id,user_firstname,user_lastname,user_occupation from incubator_db.users";

            DataTable table = new();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    Console.WriteLine("query run ok");

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult(table);
        }


        [HttpPost]
        [Route("Add")]
        public JsonResult Post(users main)
        {
            string query = @"INSERT INTO incubator_db.users (username,user_password,user_id,user_firstname,user_lastname,user_occupation) VALUES (@username,@user_password,@user_id,@user_firstname,@user_lastname,@user_occupation); ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                Console.WriteLine("Open done");
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@username", main.username);
                    myCommand.Parameters.AddWithValue("@user_password", main.user_password);
                    myCommand.Parameters.AddWithValue("@user_id", main.user_id);
                    myCommand.Parameters.AddWithValue("@user_firstname", main.user_firstname);
                    myCommand.Parameters.AddWithValue("@user_lastname", main.user_lastname);
                    myCommand.Parameters.AddWithValue("@user_occupation", main.user_occupation);
            
                    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    Console.WriteLine("query run ok");

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Added Ok");
        }
    }
}
