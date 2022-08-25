using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ChatikUsers
{
    [Key]
    public long Id { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Chats { get; set; }

}

public class ChatikChats
{
    [Key]
    public string ChatName { get; set; }
    public string ChatPass { get; set; }
    public string  Messages { get; set; }
}