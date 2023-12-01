using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Staff_Monitor_Engagement
{
    public class Database
    {
        private SQLiteConnection sqlite_conn;

        // Constructor to create and initialize the database
        public Database(string dbPath)
        {
            sqlite_conn = CreateConnection(dbPath);
            CreateTables();
        }

        // Creates a connection to the SQLite database
        private SQLiteConnection CreateConnection(string dbPath)
        {
            
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($"Data Source={dbPath}; Version=3; New=True; Compress=True;");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqlite_conn;
        }

        // Creates the tables within the database
        private void CreateTables()
        {
            SQLiteCommand sqlite_cmd;
            string[] creates = new string[]
            {
            // STUDENT Table
            "CREATE TABLE IF NOT EXISTS STUDENT (StudentID INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL UNIQUE, PasswordHash TEXT NOT NULL, FirstName TEXT, LastName TEXT);",

            // PERSONAL_SUPERVISOR Table
            "CREATE TABLE IF NOT EXISTS PERSONAL_SUPERVISOR (SupervisorID INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL UNIQUE, PasswordHash TEXT NOT NULL, FirstName TEXT, LastName TEXT);",

            // SELF_REPORTS Table
            "CREATE TABLE IF NOT EXISTS SELF_REPORTS (SelfReportID INTEGER PRIMARY KEY AUTOINCREMENT, StudentID INTEGER, ReportDate TEXT, ReportContent TEXT, FOREIGN KEY (StudentID) REFERENCES STUDENT (StudentID));",

            // MEETINGS Table
            "CREATE TABLE IF NOT EXISTS MEETINGS (MeetingID INTEGER PRIMARY KEY AUTOINCREMENT, StudentID INTEGER, SupervisorID INTEGER, MeetingDate TEXT, MeetingSubject TEXT, MeetingNotes TEXT, FOREIGN KEY (StudentID) REFERENCES STUDENT (StudentID), FOREIGN KEY (SupervisorID) REFERENCES PERSONAL_SUPERVISOR (SupervisorID));",

            // SENIOR_TUTOR Table
            "CREATE TABLE IF NOT EXISTS SENIOR_TUTOR (SeniorTutorID INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL UNIQUE, PasswordHash TEXT NOT NULL, FirstName TEXT, LastName TEXT);",

            // RELATIONSHIP Table
            "CREATE TABLE IF NOT EXISTS RELATIONSHIP (RelationshipID INTEGER PRIMARY KEY AUTOINCREMENT, StudentID INTEGER, SupervisorID INTEGER, StartDate TEXT, EndDate TEXT, FOREIGN KEY (StudentID) REFERENCES STUDENT (StudentID), FOREIGN KEY (SupervisorID) REFERENCES PERSONAL_SUPERVISOR (SupervisorID));"
            };

            foreach (var createStmt in creates)
            {
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = createStmt;
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        // You would also have other methods here to insert, update, delete, and select data from the database
        // For example:
        // Insert functions for each entity
        public void InsertStudent(string username, string passwordHash, string firstName, string lastName)
        {
            // Note: Prepared statements with parameters are used to prevent SQL injection
            using (var command = new SQLiteCommand(sqlite_conn))
            {
                command.CommandText = "INSERT INTO STUDENT (Username, PasswordHash, FirstName, LastName) VALUES (@Username, @PasswordHash, @FirstName, @LastName);";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.ExecuteNonQuery();
            }
        }
        public void InsertPersonalSupervisor(string username, string passwordHash, string firstName, string lastName)
        {
            using (var command = new SQLiteCommand(sqlite_conn))
            {
                command.CommandText = "INSERT INTO PERSONAL_SUPERVISOR (Username, PasswordHash, FirstName, LastName) VALUES (@Username, @PasswordHash, @FirstName, @LastName);";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.ExecuteNonQuery();
            }
        }
        public void InsertSelfReport(int studentId, string reportDate, string reportContent)
        {
            using (var command = new SQLiteCommand(sqlite_conn))
            {
                command.CommandText = "INSERT INTO SELF_REPORTS (StudentID, ReportDate, ReportContent) VALUES (@StudentID, @ReportDate, @ReportContent);";
                command.Parameters.AddWithValue("@StudentID", studentId);
                command.Parameters.AddWithValue("@ReportDate", reportDate);
                command.Parameters.AddWithValue("@ReportContent", reportContent);
                command.ExecuteNonQuery();
            }
        }
        public void InsertMeeting(int studentId, int supervisorId, string meetingDate, string meetingSubject, string meetingNotes)
        {
            using (var command = new SQLiteCommand(sqlite_conn))
            {
                command.CommandText = "INSERT INTO MEETINGS (StudentID, SupervisorID, MeetingDate, MeetingSubject, MeetingNotes) VALUES (@StudentID, @SupervisorID, @MeetingDate, @MeetingSubject, @MeetingNotes);";
                command.Parameters.AddWithValue("@StudentID", studentId);
                command.Parameters.AddWithValue("@SupervisorID", supervisorId);
                command.Parameters.AddWithValue("@MeetingDate", meetingDate);
                command.Parameters.AddWithValue("@MeetingSubject", meetingSubject);
                command.Parameters.AddWithValue("@MeetingNotes", meetingNotes);
                command.ExecuteNonQuery();
            }
        }
        public void InsertSeniorTutor(string username, string passwordHash, string firstName, string lastName)
        {
            using (var command = new SQLiteCommand(sqlite_conn))
            {
                command.CommandText = "INSERT INTO SENIOR_TUTOR (Username, PasswordHash, FirstName, LastName) VALUES (@Username, @PasswordHash, @FirstName, @LastName);";
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.ExecuteNonQuery();
            }
        }
        public void InsertRelationship(int studentId, int supervisorId, string startDate, string endDate)
        {
            using (var command = new SQLiteCommand(sqlite_conn))
            {
                command.CommandText = "INSERT INTO RELATIONSHIP (StudentID, SupervisorID, StartDate, EndDate) VALUES (@StudentID, @SupervisorID, @StartDate, @EndDate);";
                command.Parameters.AddWithValue("@StudentID", studentId);
                command.Parameters.AddWithValue("@SupervisorID", supervisorId);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.ExecuteNonQuery();
            }
        }
        public Student GetStudentById(int studentId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM STUDENT WHERE StudentID = @StudentID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", studentId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            Id = Convert.ToInt32(reader["StudentID"]),
                            Username = reader["Username"].ToString(),
                            // ... other properties
                            PasswordHash = reader["PasswordHash"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            
                        };
                    }
                    else
                    {
                        return null; // Or throw an exception, depending on how you want to handle not found
                    }
                }
            }
        }
        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM STUDENT";

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["StudentID"]),
                            Username = reader["Username"].ToString(),
                            // ... other properties
                            PasswordHash = reader["PasswordHash"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            
                        });
                    }
                }
            }

            return students;
        }
        public List<SelfReport> GetAllSelfReportsForStudent(int studentId)
        {
            var reports = new List<SelfReport>();

            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM SELF_REPORTS WHERE StudentID = @StudentID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", studentId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reports.Add(new SelfReport
                        {
                            Id = Convert.ToInt32(reader["SelfReportID"]),
                            StudentId = studentId,
                            ReportDate = reader["ReportDate"].ToString(),
                            ReportContent = reader["ReportContent"].ToString(),
                            // ... other properties
                            
                        });
                    }
                }
            }

            return reports;
        }
        
        // Read: Get all meetings for a student by student ID
        public List<Meeting> GetAllMeetingsForStudent(int studentId)
        {
            var meetings = new List<Meeting>();

            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM MEETINGS WHERE StudentID = @StudentID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", studentId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meetings.Add(new Meeting
                        {
                            Id = Convert.ToInt32(reader["MeetingID"]),
                            StudentId = studentId,
                            // ... other properties such as SupervisorId, MeetingDate, etc.
                            SupervisorId = Convert.ToInt32(reader["SupervisorID"]),
                            MeetingDate = reader["MeetingDate"].ToString(),
                            MeetingSubject = reader["MeetingSubject"].ToString(),
                            MeetingNotes = reader["MeetingNotes"].ToString(),
                            
                        });
                    }
                }
            }

            return meetings;
        }
        // Retrieve a personal supervisor by ID
        public PersonalSupervisor GetPersonalSupervisorById(int supervisorId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM PERSONAL_SUPERVISOR WHERE SupervisorID = @SupervisorID";
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", supervisorId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PersonalSupervisor
                        {
                            Id = Convert.ToInt32(reader["SupervisorID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(), // Be cautious with password data
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                        };
                    }
                }
            }
            return null; // Or appropriate error handling
        }

        // List all personal supervisors
        public List<PersonalSupervisor> GetAllPersonalSupervisors()
        {
            var supervisors = new List<PersonalSupervisor>();

            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM PERSONAL_SUPERVISOR";

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        supervisors.Add(new PersonalSupervisor
                        {
                            Id = Convert.ToInt32(reader["SupervisorID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                        });
                    }
                }
            }
            return supervisors;
        }

        // Retrieve a senior tutor by ID
        public SeniorTutor GetSeniorTutorById(int seniorTutorId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM SENIOR_TUTOR WHERE SeniorTutorID = @SeniorTutorID";
                sqlite_cmd.Parameters.AddWithValue("@SeniorTutorID", seniorTutorId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new SeniorTutor
                        {
                            Id = Convert.ToInt32(reader["SeniorTutorID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                        };
                    }
                }
            }
            return null; // Or appropriate error handling
        }

        // List all senior tutors
        public List<SeniorTutor> GetAllSeniorTutors()
        {
            var seniorTutors = new List<SeniorTutor>();

            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM SENIOR_TUTOR";

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seniorTutors.Add(new SeniorTutor
                        {
                            Id = Convert.ToInt32(reader["SeniorTutorID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                        });
                    }
                }
            }
            return seniorTutors;
        }
        public void UpdateStudent(Student student)
        {
            // Implementation for updating a student record
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "UPDATE STUDENT SET Username = @Username, FirstName = @FirstName, LastName = @LastName WHERE StudentID = @StudentID";
                sqlite_cmd.Parameters.AddWithValue("@Username", student.Username);
                sqlite_cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                sqlite_cmd.Parameters.AddWithValue("@LastName", student.LastName);
                sqlite_cmd.Parameters.AddWithValue("@StudentID", student.Id);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePersonalSupervisor(PersonalSupervisor personalSupervisor)
        {
            // Implementation for updating a personal supervisor record
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "UPDATE PERSONAL_SUPERVISOR SET Username = @Username, PasswordHash = @PasswordHash, FirstName = @FirstName, LastName = @LastName WHERE SupervisorID = @SupervisorID";
                sqlite_cmd.Parameters.AddWithValue("@Username", personalSupervisor.Username);
                sqlite_cmd.Parameters.AddWithValue("@PasswordHash", personalSupervisor.PasswordHash); // Ensure you're handling passwords securely
                sqlite_cmd.Parameters.AddWithValue("@FirstName", personalSupervisor.FirstName);
                sqlite_cmd.Parameters.AddWithValue("@LastName", personalSupervisor.LastName);
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", personalSupervisor.Id);

                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSelfReport(SelfReport selfReport)
        {
            // Similar implementation as UpdateStudent
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "UPDATE SELF_REPORTS SET StudentID = @StudentID, ReportDate = @ReportDate, ReportContent = @ReportContent WHERE SelfReportID = @SelfReportID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", selfReport.StudentId);
                sqlite_cmd.Parameters.AddWithValue("@ReportDate", selfReport.ReportDate);
                sqlite_cmd.Parameters.AddWithValue("@ReportContent", selfReport.ReportContent);
                sqlite_cmd.Parameters.AddWithValue("@SelfReportID", selfReport.Id);

                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMeeting(Meeting meeting)
        {
            // Similar implementation as UpdateStudent
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "UPDATE MEETINGS SET StudentID = @StudentID, SupervisorID = @SupervisorID, MeetingDate = @MeetingDate, MeetingSubject = @MeetingSubject, MeetingNotes = @MeetingNotes WHERE MeetingID = @MeetingID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", meeting.StudentId);
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", meeting.SupervisorId);
                sqlite_cmd.Parameters.AddWithValue("@MeetingDate", meeting.MeetingDate);
                sqlite_cmd.Parameters.AddWithValue("@MeetingSubject", meeting.MeetingSubject);
                sqlite_cmd.Parameters.AddWithValue("@MeetingNotes", meeting.MeetingNotes);
                sqlite_cmd.Parameters.AddWithValue("@MeetingID", meeting.Id);

                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSeniorTutor(SeniorTutor seniorTutor)
        {
            // Similar implementation as UpdateStudent
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "UPDATE SENIOR_TUTOR SET Username = @Username, PasswordHash = @PasswordHash, FirstName = @FirstName, LastName = @LastName WHERE SeniorTutorID = @SeniorTutorID";
                sqlite_cmd.Parameters.AddWithValue("@Username", seniorTutor.Username);
                sqlite_cmd.Parameters.AddWithValue("@PasswordHash", seniorTutor.PasswordHash); // Ensure secure handling of passwords
                sqlite_cmd.Parameters.AddWithValue("@FirstName", seniorTutor.FirstName);
                sqlite_cmd.Parameters.AddWithValue("@LastName", seniorTutor.LastName);
                sqlite_cmd.Parameters.AddWithValue("@SeniorTutorID", seniorTutor.Id);

                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRelationship(Relationship relationship)
        {
            // Similar implementation as UpdateStudent
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "UPDATE RELATIONSHIP SET StudentID = @StudentID, SupervisorID = @SupervisorID, StartDate = @StartDate, EndDate = @EndDate WHERE RelationshipID = @RelationshipID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", relationship.StudentId);
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", relationship.SupervisorId);
                sqlite_cmd.Parameters.AddWithValue("@StartDate", relationship.StartDate);
                sqlite_cmd.Parameters.AddWithValue("@EndDate", relationship.EndDate);
                sqlite_cmd.Parameters.AddWithValue("@RelationshipID", relationship.Id);

                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void DeleteStudent(int studentId)
        {
            // Implementation for deleting a student record
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "DELETE FROM STUDENT WHERE StudentID = @StudentID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", studentId);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void DeletePersonalSupervisor(int supervisorId)
        {
            // Implementation for deleting a personal supervisor record
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "DELETE FROM PERSONAL_SUPERVISOR WHERE SupervisorID = @SupervisorID";
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", supervisorId);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSelfReport(int selfReportId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "DELETE FROM SELF_REPORTS WHERE SelfReportID = @SelfReportID";
                sqlite_cmd.Parameters.AddWithValue("@SelfReportID", selfReportId);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void DeleteMeeting(int meetingId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "DELETE FROM MEETINGS WHERE MeetingID = @MeetingID";
                sqlite_cmd.Parameters.AddWithValue("@MeetingID", meetingId);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSeniorTutor(int seniorTutorId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "DELETE FROM SENIOR_TUTOR WHERE SeniorTutorID = @SeniorTutorID";
                sqlite_cmd.Parameters.AddWithValue("@SeniorTutorID", seniorTutorId);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void DeleteRelationship(int relationshipId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "DELETE FROM RELATIONSHIP WHERE RelationshipID = @RelationshipID";
                sqlite_cmd.Parameters.AddWithValue("@RelationshipID", relationshipId);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public UserRole Login(string username, string plaintextPassword)
        {
            if (IsUserValid(username, plaintextPassword, "STUDENT"))
            {
                return UserRole.Student;
            }
            else if (IsUserValid(username, plaintextPassword, "PERSONAL_SUPERVISOR"))
            {
                return UserRole.PersonalSupervisor;
            }
            else if (IsUserValid(username, plaintextPassword, "SENIOR_TUTOR"))
            {
                return UserRole.SeniorTutor;
            }
            else
            {
                return UserRole.None;
            }
        }

        public bool IsUserValid(string username, string plaintextPassword, string tableName)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = $"SELECT COUNT(1) FROM {tableName} WHERE Username = @Username AND PasswordHash = @Password";
                sqlite_cmd.Parameters.AddWithValue("@Username", username);
                sqlite_cmd.Parameters.AddWithValue("@Password", plaintextPassword); // Assuming plaintextPassword for testing

                int userCount = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                return userCount > 0;
            }
        }
        public int GetStudentIdByUsername(string username)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT StudentID FROM STUDENT WHERE Username = @Username";
                sqlite_cmd.Parameters.AddWithValue("@Username", username);

                var result = sqlite_cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Or handle this case as appropriate (e.g., throw an exception)
                }
            }
        }
        public int GetSupervisorIdByStudentId(int studentId)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT SupervisorID FROM RELATIONSHIP WHERE StudentID = @StudentID";
                sqlite_cmd.Parameters.AddWithValue("@StudentID", studentId);

                var result = sqlite_cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Or handle this case as appropriate
                }
            }
        }
        public int GetSupervisorIdByUsername(string username)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT SupervisorID FROM PERSONAL_SUPERVISOR WHERE Username = @Username";
                sqlite_cmd.Parameters.AddWithValue("@Username", username);

                var result = sqlite_cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1; // Or handle this case as appropriate
                }
            }
        }
        public List<Student> GetStudentsBySupervisorId(int supervisorId)
        {
            List<Student> students = new List<Student>();
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM STUDENT INNER JOIN RELATIONSHIP ON STUDENT.StudentID = RELATIONSHIP.StudentID WHERE RELATIONSHIP.SupervisorID = @SupervisorID";
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", supervisorId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            // Assuming Student class has properties like Id, FirstName, LastName, etc.
                            Id = Convert.ToInt32(reader["StudentID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            // ... other properties
                        });
                    }
                }
            }
            return students;
        }
        public List<Meeting> GetMeetingsBySupervisorId(int supervisorId)
        {
            List<Meeting> meetings = new List<Meeting>();
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM MEETINGS WHERE SupervisorID = @SupervisorID";
                sqlite_cmd.Parameters.AddWithValue("@SupervisorID", supervisorId);

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meetings.Add(new Meeting
                        {
                            // Assuming Meeting class has properties like Id, StudentId, MeetingDate, etc.
                            Id = Convert.ToInt32(reader["MeetingID"]),
                            StudentId = Convert.ToInt32(reader["StudentID"]),
                            MeetingDate = reader["MeetingDate"].ToString(),
                            MeetingSubject = reader["MeetingSubject"].ToString(),
                            MeetingNotes = reader["MeetingNotes"].ToString(),
                            // ... other properties
                        });
                    }
                }
            }
            return meetings;
        }
        public List<Meeting> GetAllMeetings()
        {
            List<Meeting> meetings = new List<Meeting>();
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM MEETINGS";

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meetings.Add(new Meeting
                        {
                            // Assuming Meeting class has properties like Id, StudentId, SupervisorId, MeetingDate, etc.
                            Id = Convert.ToInt32(reader["MeetingID"]),
                            StudentId = Convert.ToInt32(reader["StudentID"]),
                            SupervisorId = Convert.ToInt32(reader["SupervisorID"]),
                            MeetingDate = reader["MeetingDate"].ToString(),
                            MeetingSubject = reader["MeetingSubject"].ToString(),
                            MeetingNotes = reader["MeetingNotes"].ToString(),
                            // ... other properties
                        });
                    }
                }
            }
            return meetings;
        }
        public List<SelfReport> GetAllSelfReports()
        {
            List<SelfReport> selfReports = new List<SelfReport>();
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT * FROM SELF_REPORTS";

                using (var reader = sqlite_cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        selfReports.Add(new SelfReport
                        {
                            Id = Convert.ToInt32(reader["SelfReportID"]),
                            StudentId = Convert.ToInt32(reader["StudentID"]),
                            ReportDate = reader["ReportDate"].ToString(),
                            ReportContent = reader["ReportContent"].ToString()
                        });
                    }
                }
            }
            return selfReports;
        }
        public void AddNewStudent(string username, string passwordHash, string firstName, string lastName)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "INSERT INTO STUDENT (Username, PasswordHash, FirstName, LastName) VALUES (@Username, @PasswordHash, @FirstName, @LastName);";
                sqlite_cmd.Parameters.AddWithValue("@Username", username);
                sqlite_cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                sqlite_cmd.Parameters.AddWithValue("@FirstName", firstName);
                sqlite_cmd.Parameters.AddWithValue("@LastName", lastName);

                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void AddNewPersonalSupervisor(string username, string passwordHash, string firstName, string lastName)
        {
            using (var sqlite_cmd = sqlite_conn.CreateCommand())
            {
                sqlite_cmd.CommandText = "INSERT INTO PERSONAL_SUPERVISOR (Username, PasswordHash, FirstName, LastName) VALUES (@Username, @PasswordHash, @FirstName, @LastName);";
                sqlite_cmd.Parameters.AddWithValue("@Username", username);
                sqlite_cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                sqlite_cmd.Parameters.AddWithValue("@FirstName", firstName);
                sqlite_cmd.Parameters.AddWithValue("@LastName", lastName);

                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }
}
