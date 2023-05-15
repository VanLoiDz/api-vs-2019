using API_COMIC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_COMIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicGenreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ComicGenreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select id, genre_name, describe from ComicGenre";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("COMIC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(ComicGenre comicgenre)
        {
            string query = @"Insert into ComicGenre values
                         (N'" + comicgenre.genre_name + "' " +
                         ", N'" + comicgenre.describe + "' " + 
                         ")";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("COMIC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Thêm mới thành công");
        }

        [HttpPut]
        public JsonResult Put(ComicGenre comicgenre)
        {
            string query = @"Update ComicGenre set 
                    genre_name = N'" + comicgenre.genre_name + "' "
                  + ", describe   = N'" + comicgenre.describe + "' " +
                   " where id = " + comicgenre.id;
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("COMIC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Cập nhật thành công");
        }

        [HttpDelete("{ma}")]
        public JsonResult Delete(int ma)
        {
            string query = @"Delete from ComicGenre " + " where id = " + ma;
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("COMIC");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Xoá thành công");
        }
    }
}
