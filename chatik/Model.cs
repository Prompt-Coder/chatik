using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ChatikUsers
{
    [Key]
    public long Id { get; set; }
    public string user { get; set; }
    public string password { get; set; }
    public string chats { get; set; }

}

public class ChatikChats
{
    [Key]
    public string chatName { get; set; }
    public string chatPass { get; set; }
    public string  messages { get; set; }
}