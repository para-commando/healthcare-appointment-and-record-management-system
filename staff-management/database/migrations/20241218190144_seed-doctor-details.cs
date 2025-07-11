using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using staff_management.database.models;
 using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace staffmanagement.database.migrations
{
    /// <inheritdoc />
    public partial class seeddoctordetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {   migrationBuilder.CreateTable(
                name: "doctor-details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    doctor_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    doctor_specialization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    doctor_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    doctor_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    doctor_unique_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    doctor_registration_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor-details", x => x.id);
                });
         
            using FileStream fs = new FileStream("database/data/doctor_details.csv", FileMode.Open, FileAccess.Read, FileShare.None);
            using StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            string line = string.Empty;
            string[] data = Array.Empty<string>();
            List<DoctorDetails> clinicalHealthStaticDataList = new List<DoctorDetails>();
            DoctorDetails clinicalHealthStaticData = null;
            sr.ReadLine();
            int j = 1;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                data = line.Split(",");

                clinicalHealthStaticData = new DoctorDetails
                {
                    Id = Int32.Parse(data[0]),
                    DoctorName = data[1],
                    DoctorSpecialization = data[2],
                    DoctorAddress = data[3],
                    DoctorContact = data[4],
                    DoctorUniqueId = data[5],
                    DoctorDateOfJoining = DateOnly.Parse(data[6])

                };

                clinicalHealthStaticDataList.Add(clinicalHealthStaticData);
            }
            var bulkInsertData = new object[clinicalHealthStaticDataList.Count, 7];
            for (int i = 0; i < clinicalHealthStaticDataList.Count; i++)
            {
                bulkInsertData[i, 0] = clinicalHealthStaticDataList[i].Id;
                bulkInsertData[i, 1] = clinicalHealthStaticDataList[i].DoctorName;
                bulkInsertData[i, 2] = clinicalHealthStaticDataList[i].DoctorSpecialization;
                bulkInsertData[i, 3] = clinicalHealthStaticDataList[i].DoctorAddress;
                bulkInsertData[i, 4] = clinicalHealthStaticDataList[i].DoctorContact;
                bulkInsertData[i, 5] = clinicalHealthStaticDataList[i].DoctorUniqueId;
                bulkInsertData[i, 6] = clinicalHealthStaticDataList[i].DoctorDateOfJoining;

            }

            migrationBuilder.InsertData(
                table: "doctor-details",
                columns: new[] { "id", "doctor_name","doctor_specialization", "doctor_address", "doctor_contact", "doctor_unique_id", "doctor_registration_date" },
                values: bulkInsertData);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("truncate table doctor-details");  
            migrationBuilder.DropTable(
                name: "doctor-details");

        }
    }
}
