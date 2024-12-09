namespace clinical_data_grid.database.models;

public record User(
    int Id,
    string Username,
    string Name,
    string Email,
    string Password,
    string[] Roles);