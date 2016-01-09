var fs = require('fs');
var csv = require('ya-csv');
var async = require('async');
var http = require('http');
var sql = require('msnodesql');
var http = require('http');

var useTrustedConnection = false;
var conn_str = "Driver={SQL Server Native Client 11.0};Server=tcp:jzti6rpvpp.database.windows.net;" +
(useTrustedConnection == true ? "Trusted_Connection={Yes};" : "UID=dasl_import;PWD=BjEsjy7fxY-7!4xr;") +
"Database={DASLImport_db};";

sql.open(conn_str, function (err, conn) {
    if (err) {
        console.log("Error opening the connection!");
        return;
    }
    else {
        console.log("Successfuly connected");
        
        var inputFile='hs_data.csv';
        var reader = csv.createCsvFileReader(inputFile, { columnsFromHeader: true });
        reader.addListener('data', function(data) {
            conn.queryRaw(
                "select s.ID as StudentId, c.ID as ClassId from students s " +
                "inner join classes c on s.StudentId = '" + data.StudentId + "' and c.CourseCode = '" + data.CourseCode + "' and c.SectionNumber = '" + data.SectionNumber + "';",
                function(err, results) {
                    if(err) {
                        console.log(err);
                        return;
                    }
                    var query = "INSERT INTO StudentClasses (StudentId, ClassId) " +
                        "VALUES ('" + results.rows[0][0] + "','" + results.rows[0][1] + "');"
                    conn.queryRaw(
                        query,
                        function(err, results) {
                            if(err) {
                                console.log(err);
                                return;
                            }
                            console.log(results);
                        }
                    );
                }
            );
            
        });
        reader.on('end', function() {
            console.log('done');
        });
    }
});


// var openDatabase = function() {
    
// };