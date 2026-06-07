public bool AddUserWithPassword(string username, string passwordHash, string role)
{
    using (var conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        string query = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@Username, @PasswordHash, @Role)";
        using (var cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@Role", role);
            cmd.ExecuteNonQuery();
        }
    }
    return true;
}

public User GetUserByUsername(string username)
{
    using (var conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        string query = "SELECT UserID, Username, Email FROM Users WHERE Username = @Username";
        using (var cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Username", username);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        UserID = reader.GetInt32("UserID"),
                        Username = reader.GetString("Username"),
                        Email = reader.GetString("Email")
                    };
                }
            }
        }
    }
    return null;
}
