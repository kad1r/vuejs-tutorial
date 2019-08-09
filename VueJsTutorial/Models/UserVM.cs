using System;
using System.Collections.Generic;

namespace VueJsTutorial.Models
{
	public class UserVM
	{
		public User User { get; set; }
		public ICollection<User> List { get; set; }
	}

	public class User
	{
		public string Email { get; set; }
		public DateTime Dob { get; set; }
		public int Age { get; set; }
	}
}
