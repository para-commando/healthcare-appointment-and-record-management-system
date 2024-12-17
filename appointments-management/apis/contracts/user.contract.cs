namespace appointment_details.apis.contracts;

public record User(
    int Id,
    string Username,
    string Name,
    string Email,
    string Password,
    string Department,
    string[] Roles);