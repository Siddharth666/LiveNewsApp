using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace NewsApp.Services
{
    public class StoreData
    {
        public bool Store(List<News> news)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-3MTH60PE\SQLEXPRESS;Initial Catalog=UniversityDB;Integrated Security=True");

            int count = 0;
            foreach (var eachNews in news)
            {
                con.Open();
                count++;
                

                string query = "INSERT INTO AllNews2 (Name, Title, Description, Url, UrlToImage, PublishedAt, Content) VALUES(@Name, @Title, @Description, @Url, @UrlToImage, @PublishedAt, @Content)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", eachNews.Name);
                cmd.Parameters.AddWithValue("@Title", eachNews.Title);
                cmd.Parameters.AddWithValue("@Description", eachNews.Description);
                cmd.Parameters.AddWithValue("@Url", eachNews.Url);
                cmd.Parameters.AddWithValue("@UrlToImage", eachNews.UrlToImage);
                cmd.Parameters.AddWithValue("@PublishedAt", eachNews.PublishedAt);
                cmd.Parameters.AddWithValue("@Content", eachNews.Content);

                //2nd commnent, boy, I'm drunk.......

                if (count > 4)
                {
                    con.Close();
                    break;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Records Inserted Successfully");
                    
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();


                }

            }
            
         
            return true;
        }
    }
}
