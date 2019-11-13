using System;
using System.Collections.Generic;
using System.Text;

namespace EventCalendar.Entities
{

    /// <summary>
    /// Person kann sowohl zu einer Veranstaltung einladen,
    /// als auch an Veranstaltungen teilnehmen
    /// </summary>
    public class Person : IComparable<Person>
    {
        private int _participatingNrOfEv;
        public string LastName { get; }
        public string FirstName { get; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int ParticipatingNrOfEv 
        { get => _participatingNrOfEv;
          set => _participatingNrOfEv = value;
        }



        public Person(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public int CompareTo(Person other)
        {
            int comp  = (_participatingNrOfEv.CompareTo(other._participatingNrOfEv)) * -1;
            if (comp == 0) 
            {
                return FirstName.CompareTo(other.FirstName);
            }
            if (comp == 0)
            {
                return LastName.CompareTo(other.LastName);
            }

            else
            {
                return comp;
            }
            
            
        }
    }
}