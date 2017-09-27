using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Student_Client_Web_Database
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        string conn =
            "Server = tcp:andersstandardserver2017.database.windows.net,1433; Initial Catalog = andersstandarddatabase2017; Persist Security Info = False; User ID = andersstandardserver2017; Password =Pass1234; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";






        private static Student ReadStudent(IDataRecord reader)
        {
            int tempId = reader.GetInt32(0);
            string tempNavn = reader.GetString(1);
            string tempKlasse = reader.GetString(2);

            Student tempStudent = new Student { ID = tempId, Navn = tempNavn, Klasse=tempKlasse};
            return tempStudent;
        }






        public IList<Student> GetAllStudents()
        {
            string selectAllStudents = "select * from students";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllStudents, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Student> studentList=new List<Student>();
                        while (reader.Read())
                        {
                            Student studentRead = ReadStudent(reader);
                            studentList.Add(studentRead);
                        }
                        return studentList;
                    }
                }
            }
        }





        public Student GetStudentByID(int studID)
        {
            const string selectStudent = "select * from students where ID=@studIDtemp";
            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStudent, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@studIDtemp", studID);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;
                        reader.Read(); // Advance cursor to first row
                        Student tempStudent = ReadStudent(reader);
                        return tempStudent;
                    }
                }
            }
        }




        public IList<Student> GetStudentsByName(string name)
        {
            string selectStr = "select * from students where Navn LIKE @name";
            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        IList<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student st = ReadStudent(reader);
                            studentList.Add(st);
                        }
                        return studentList;
                    }
                }
            }
        }





        public void AddStudent(int id, string navn, string klasse)
        {
            const string insertStudent = "insert into students values (@id, @navn, @klasse)";
            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertStudent, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    insertCommand.Parameters.AddWithValue("@navn", navn);
                    insertCommand.Parameters.AddWithValue("@klasse", klasse);
                    int rowsAffected = insertCommand.ExecuteNonQuery();   //rowsAffected kan evt returneres
                }
            }
        }








        
    }
}
