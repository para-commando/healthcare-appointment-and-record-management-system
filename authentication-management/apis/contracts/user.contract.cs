namespace authentication_management.database.models;

public record User(
    int Id,
    string Username,
    string Name,
    string Email,
    string Password,
    string Department,
    string[] Roles);