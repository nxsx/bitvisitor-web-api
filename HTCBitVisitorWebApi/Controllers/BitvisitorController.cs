using Microsoft.AspNetCore.Mvc;
using HTCBitVisitorWebApi.Models;
using System.Data.OleDb;

namespace HTCBitVisitorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BitvisitorController : ControllerBase
{
    private readonly string _bitVistorConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\OpenDB.mdb;";

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BitvisitorData>>> Get()
    {
        var bitVisitors = new List<BitvisitorData>();
        using (var connection = new OleDbConnection(_bitVistorConnectionString))
        {
            await connection.OpenAsync();

            String _commandString = String.Format("SELECT [Row_ID], [Visitor_Type], [Date_Time], [Visitor_ID], [Visitor_Name], [Visitor_Company], [Visitor_Mobile], [Vehicle_ID], [Host_Department], [Host_Name] FROM [Transaction] WHERE [In_Out] = 1 AND [Machine_No] = 2 AND [Modified_By] IS NULL");
            var command = new OleDbCommand(_commandString, connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    bitVisitors.Add(new BitvisitorData
                    {
                        Id = reader.GetInt32(0),
                        VisitorType = reader.GetString(1),
                        VisitorTime = reader.GetString(2),
                        VisitorId = reader.GetInt32(3),
                        VisitorName = reader.GetString(4),
                        VisitorComp = reader.GetString(5),
                        VisitorMobile = reader.GetString(6),
                        VisitorVehicle = reader.GetString(7),
                        VisitorHostDeparture = reader.GetString(8),
                        VisitorHostName = reader.GetString(9)
                    });
                }
            }
        }

        return bitVisitors;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BitvisitorData>> GetById(int id)
    {
        var bitVisitor = new BitvisitorData();

        using (var connection = new OleDbConnection(_bitVistorConnectionString))
        {
            await connection.OpenAsync();

            String _commandString = string.Format("SELECT [Row_ID], [Visitor_Type], [Date_Time], [Visitor_ID], [Visitor_Name], [Visitor_Company], [Visitor_Mobile], [Vehicle_ID], [Host_Department], [Host_Name] FROM [Transaction] WHERE [Row_ID] = @Id");
            var command = new OleDbCommand(_commandString, connection);
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                await reader.ReadAsync();

                bitVisitor.Id = reader.GetInt32(0);
                bitVisitor.VisitorType = reader.GetString(1);
                bitVisitor.VisitorTime = reader.GetString(2);
                bitVisitor.VisitorId = reader.GetInt32(3);
                bitVisitor.VisitorName = reader.GetString(4);
                bitVisitor.VisitorComp = reader.GetString(5);
                bitVisitor.VisitorMobile = reader.GetString(6);
                bitVisitor.VisitorVehicle = reader.GetString(7);
                bitVisitor.VisitorHostDeparture = reader.GetString(8);
                bitVisitor.VisitorHostName = reader.GetString(9);
            }
        }

        return bitVisitor;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BitvisitorData bitvisitor)
    {
        using (var connection = new OleDbConnection(_bitVistorConnectionString))
        {
            await connection.OpenAsync();

            String _commandString = String.Format("INSERT INTO [Transaction] ([Visitor_Type], [Date_Time], [Visitor_ID], [Visitor_Name], [Visitor_Company], [Visitor_Mobile], [Vehicle_ID], [Host_Department], [Host_Name]) VALUES (@VisitorType, @VisitorTime, @VisitorId, @VisitorName, @VisitorComp, @VisitorMobile, @VisitorVehicle, @VisitorHostDeparture, @VisitorHostName)");
            var command = new OleDbCommand(_commandString, connection);

            command.Parameters.AddWithValue("@VisitorType", bitvisitor.VisitorType);
            command.Parameters.AddWithValue("@VisitorId", bitvisitor.VisitorId);
            command.Parameters.AddWithValue("@VisitorName", bitvisitor.VisitorName);
            command.Parameters.AddWithValue("@VisitorComp", bitvisitor.VisitorComp);
            command.Parameters.AddWithValue("@VisitorMobile", bitvisitor.VisitorMobile);
            command.Parameters.AddWithValue("@VisitorVehicle", bitvisitor.VisitorVehicle);
            command.Parameters.AddWithValue("@VisitorHostDeparture", bitvisitor.VisitorHostDeparture);
            command.Parameters.AddWithValue("@VisitorHostName", bitvisitor.VisitorHostName);
            await command.ExecuteNonQueryAsync();
        }

        return CreatedAtAction(nameof(Get), new { id = bitvisitor.Id }, bitvisitor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BitvisitorData bitvisitor)
    {
        using (var connection = new OleDbConnection(_bitVistorConnectionString))
        {
            await connection.OpenAsync();

            String _commandString = string.Format("UPDATE [Transaction] SET [Visitor_Type] = @VisitorType, [Date_Time] = @VisitorTime, [Visitor_ID] = @VisitorId, [Visitor_Name] = @VisitorName, [Visitor_Company] = @VisitorComp, [Visitor_Mobile] = @VisitorMobile, [Vehicle_ID] = @VisitorVehicle, [Host_Department] = @VisitorHostDeparture, [Host_Name] = @VisitorHostName WHERE [Row_ID] = @Id");
            var command = new OleDbCommand(_commandString, connection);

            command.Parameters.AddWithValue("@VisitorType", bitvisitor.VisitorType);
            command.Parameters.AddWithValue("@VisitorTime", bitvisitor.VisitorTime);
            command.Parameters.AddWithValue("@VisitorId", bitvisitor.VisitorId);
            command.Parameters.AddWithValue("@VisitorName", bitvisitor.VisitorName);
            command.Parameters.AddWithValue("@VisitorComp", bitvisitor.VisitorComp);
            command.Parameters.AddWithValue("@VisitorMobile", bitvisitor.VisitorMobile);
            command.Parameters.AddWithValue("@VisitorVehicle", bitvisitor.VisitorVehicle);
            command.Parameters.AddWithValue("@VisitorHostDeparture", bitvisitor.VisitorHostDeparture);
            command.Parameters.AddWithValue("@VisitorHostName", bitvisitor.VisitorHostName);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        using (var connection = new OleDbConnection(_bitVistorConnectionString))
        {
            await connection.OpenAsync();

            String _commandString = string.Format("DELETE FROM [Transaction] WHERE [Row_ID] = @Id");
            var command = new OleDbCommand(_commandString, connection);

            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }

        return NoContent();
    }

}
