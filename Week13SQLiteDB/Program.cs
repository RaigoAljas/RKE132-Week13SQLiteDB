﻿
using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());
FindCustomer();
static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    //connection.Close();
    return connection;
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    //command = myConnection.CreateCommand();
    //command.CommandText = "SELECT customer.firstName, customer.lastName, status.statustype " +
    //    "FROM customerStatus " +
    //    "JOIN customer on customer.rowid = customerStatus.customerId " +
    //    "JOIN status ON status.rowid = customerStatus.statusId " +
    //    "ORDER BY status.statustype";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        //string readerRowid = reader.GetString(0);
        string readerStringFirstName = reader.GetString(1); 
        string readerStringLastName = reader.GetString(2);
        string readerStringStatus = reader.GetString(3);

        Console.WriteLine($"{readerRowid}. Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringStatus}");
        //Console.WriteLine($"Full name: {readerStringFirstName} {readerStringLastName}; Status: {readerStringStatus}");
    }

    myConnection.Close();
}


static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
    $"VALUES ('{fName}', '{lName}', '{dob}' )";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted{rowInserted}");

    //myConnection.Close();

    ReadData(myConnection);


}

static void RemoveCustomer(SQLiteConnection myConnection)
{ 
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer");

    ReadData(myConnection);
}

static void FindCustomer()
{ 

}