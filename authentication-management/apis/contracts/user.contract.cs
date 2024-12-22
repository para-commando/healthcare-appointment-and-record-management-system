namespace authentication_management.database.models;

public record User(
    string StaffUniqueId,
    string Username,
    string Department,
    string[] Roles);