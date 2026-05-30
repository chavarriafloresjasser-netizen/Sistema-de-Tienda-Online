using System;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
class DataBaseConnetionShop
{
    //Dirección de coneccion de la base de datos
    private string connectionString = "";
    //Coneccion con la base de datos
    private SqlConnection connect;
    //Metodo de conección de la base de datos
    private void Connect()
    {
        connect = new SqlConnection(connectionString);
    }
    //Constructor
    public DataBaseConnetionShop()
    {
        Connect();
    }
}