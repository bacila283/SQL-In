using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
	public class ConnecttoDB
	{
		//string connectionString = "Server=DESKTOP-JOD222T\\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;";
		string connectionString = "Server=localhost\\SQLEXPRESS;Database=MainDB;Trusted_Connection=True;TrustServerCertificate=True";

		public async void Main(string login, string password)
		{
			//SqlParameter sqlParameter = new SqlParameter();


			// Создание подключения
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Task task = connection.OpenAsync();
				task.Wait();
				string sqlex = $"Select * from Users where Users.login = '{login}' and Users.password = '{password}'";
				//string sqlex = "Select * from Users where Users.login = @login and Users.password = @password";
				SqlCommand command = new SqlCommand(sqlex, connection);
				//param
				SqlParameter sqlParameter = new SqlParameter();
				sqlParameter = new SqlParameter("@login", login.ToString());
				command.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@password", password.ToString());
				command.Parameters.Add(sqlParameter);
				//param
				//command.CommandText = $"Select * from Users where Users.login = '{login}' and Users.password = '{password}'";//default
				command.Connection = connection;
				await command.ExecuteNonQueryAsync();
				SqlDataReader reader = await command.ExecuteReaderAsync();
				if (reader.HasRows)
				{
					
					Console.WriteLine("Учетка есть!");
					while (reader.Read())
					{
					 ReadSingleRow((IDataRecord)reader);
					}
					
					if(reader.NextResult()==true) {
						while (reader.Read())
						{
							ReadSingleRow((IDataRecord)reader);
							ReadSingleRow((IDataRecord)reader);
						}
					}
				}
				else
				{
					Console.WriteLine("Учетная запись не найдена!");
				}

				//}Select Users.email from Users where Users.login='bacila' and Users.password = '12345'

				Console.WriteLine("Подключение открыто");
			}

			Console.WriteLine("Программа завершила работу.");
			//Console.Read();
		}
		private static string ReadSingleRow(IDataRecord record)
		{
			Console.WriteLine(String.Format("{0}, {1}, {2}", record[0], record[1], record[2]));
			return String.Format("{0}, {1}, {2}", record[0], record[1], record[2]);
		}
	}
}
