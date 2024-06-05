using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCategoriesApp
{
    public record Category
    {
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; }
    }
    public class ManageCategories
    {
        SqlConnection connection;
        SqlCommand command;
        string ConnectionString = "Server=.;uid=sa;pwd=sa;database=MyStore;Encrypt=True;Trust Server Certificate=True";
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            connection = new SqlConnection(ConnectionString);
            string sql = "Select CategoryId, CategoryName from Categories";
            command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32("CategoryId"),
                            CategoryName = reader.GetString("CategoryName")
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            { connection.Close(); }
            return categories;
        } 

        public void InSertCategory(Category category)
        {
            connection = new SqlConnection(ConnectionString);
            command = new SqlCommand("Insert Categories values(@CategoryName)", connection);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
            finally { connection.Close(); }

        }

        public void UpdateCategory(Category category)
        {
            connection = new SqlConnection(ConnectionString);
            string sql = "Update Categories set CategoryName = @CategoryName where CategoryID = @CategoryID";
            command = new SqlCommand (sql, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
        }

        public void DeleteCategory(Category category)
        {
            connection = new SqlConnection(ConnectionString);
            string sql = "Delete Categories where CategoryID = @CategoryID";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
        }

    }
}
