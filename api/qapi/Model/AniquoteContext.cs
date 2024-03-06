namespace qapi.Model;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

public class AniquoteContext : DbContext
{
    public DbSet<Aniquote> Aniquote { get; set; }
    public string DbPath { get; }

    public AniquoteContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "quotimal.db");
        Console.WriteLine(DbPath);
        SqliteConnection connection = new SqliteConnection($"Data Source={DbPath}");
        // ########### FYI THE DB is created when it is OPENED ########
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        FileInfo fi = new FileInfo(DbPath);
        if (fi.Length == 0){
            foreach (String tableCreate in allTableCreation){
                command.CommandText = tableCreate;
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine(connection.DataSource);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    protected String [] allTableCreation = {
        @"CREATE TABLE Aniquote
            ( [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            [ImageLink] NVARCHAR(250)  NOT NULL check(length(ImageLink) <= 250),
            [InfoLink] NVARCHAR(250)  NOT NULL check(length(InfoLink) <= 250),
            [Quote] NVARCHAR(750)  NOT NULL check(length(Quote) <= 750),
            [Author] NVARCHAR(200)  NOT NULL check(length(Author) <= 200),
            [AuthorLink] NVARCHAR(250)  NOT NULL check(length(AuthorLink) <= 250),
            [DayNumber] INTEGER NOT NULL default 0
            )"
    };
}