using System.Text;
using clinical_data_grid.database.models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinical_data_grid.database.migrations
{
    /// <inheritdoc />
    public partial class seed_clinical_health_static_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using FileStream fs = new FileStream("database/data/clinical_grid_data-Medicine_Details.csv", FileMode.Open, FileAccess.Read, FileShare.None);
            using StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            string line = string.Empty;
            string[] data = Array.Empty<string>();
            List<ClinicalHealthStaticData> clinicalHealthStaticDataList = new List<ClinicalHealthStaticData>();
            ClinicalHealthStaticData clinicalHealthStaticData = null;
            sr.ReadLine();
            int j = 1;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                data = line.Split(",");

                clinicalHealthStaticData = new ClinicalHealthStaticData
                {
                    Id = j++,
                    Disease = data[0],
                    Composition = data[1],
                    MedicineName = data[2],
                    Uses = data[3],
                    SideEffects = data[4]
                };

                clinicalHealthStaticDataList.Add(clinicalHealthStaticData);
            }
            var bulkInsertData = new object[clinicalHealthStaticDataList.Count, 6];
            for (int i = 0; i < clinicalHealthStaticDataList.Count; i++)
            {
                bulkInsertData[i, 0] = clinicalHealthStaticDataList[i].Id;
                bulkInsertData[i, 1] = clinicalHealthStaticDataList[i].Disease;
                bulkInsertData[i, 2] = clinicalHealthStaticDataList[i].Composition;
                bulkInsertData[i, 3] = clinicalHealthStaticDataList[i].MedicineName;
                bulkInsertData[i, 4] = clinicalHealthStaticDataList[i].Uses;
                bulkInsertData[i, 5] = clinicalHealthStaticDataList[i].SideEffects;
            }

            migrationBuilder.InsertData(
                table: "clinical_health_static_data",
                columns: new[] { "id", "disease", "composition", "medicine_name", "uses", "side_effects" },
                values: bulkInsertData);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("truncate table clinical_health_static_data");
        }
    }
}
