using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMusic.Data.Migrations
{
    public partial class SeedMusicsAndArtistsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Artists (Name, Id) Values ('Linkin Park',0)");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name, Id) Values ('Iron Maiden',1)");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name, Id) Values ('Flogging Molly',2)");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name, Id) Values ('Red Hot Chilli Peppers', 3)");
                
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('In The End', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'), 0)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Numb', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'), 1)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Breaking The Habit', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'), 2)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Fear of the dark', (SELECT Id FROM Artists WHERE Name = 'Iron Maiden'), 3)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Number of the beast', (SELECT Id FROM Artists WHERE Name = 'Iron Maiden'), 4)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('The Trooper', (SELECT Id FROM Artists WHERE Name = 'Iron Maiden'), 5)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('What''s left of the flag', (SELECT Id FROM Artists WHERE Name = 'Flogging Molly'), 6)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Drunken Lullabies', (SELECT Id FROM Artists WHERE Name = 'Flogging Molly'), 7)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('If I Ever Leave this World Alive', (SELECT Id FROM Artists WHERE Name = 'Flogging Molly'), 8)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Californication', (SELECT Id FROM Artists WHERE Name = 'Red Hot Chilli Peppers'), 9)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Tell Me Baby', (SELECT Id FROM Artists WHERE Name = 'Red Hot Chilli Peppers'), 10)");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId, Id) Values ('Parallel Universe', (SELECT Id FROM Artists WHERE Name = 'Red Hot Chilli Peppers'), 11)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Musics");

            migrationBuilder
                .Sql("DELETE FROM Artists");
        }
    }
}
