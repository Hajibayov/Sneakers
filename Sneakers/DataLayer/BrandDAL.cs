using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Sneakers.Models;
using System.Collections.Generic;
using System.Data;

namespace Sneakers.DataLayer
{
    public class BrandDAL
    {
        public string cnn = "";

        public BrandDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).
                AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:DefaultConncetion").Value;
        }
        public List<SNEAKERS_BRAND> GetAllBrands()
        {
            List<SNEAKERS_BRAND> ListOfBrands = new List<SNEAKERS_BRAND>();
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllBrands", cn))
                {
                    if(cn.State == System.Data.ConnectionState.Closed)
                        cn.Open();

                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ListOfBrands.Add(new SNEAKERS_BRAND()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Brand = reader["Brand"].ToString()
                        });
                    }
                }
            }
            return ListOfBrands;
        }
    }
}
