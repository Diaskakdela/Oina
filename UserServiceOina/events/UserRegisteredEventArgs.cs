using UserServiceOina.entity;

namespace UserServiceOina.events;

public class UserRegisteredEventArgs : EventArgs
{
    public User User { get; set; }
}