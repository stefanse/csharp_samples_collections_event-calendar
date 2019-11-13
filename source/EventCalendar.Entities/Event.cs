using System;
using System.Collections.Generic;

namespace EventCalendar.Entities
{
    public class Event 
    {
        private DateTime _dateTime;
        private int _maxParticipators;
        public List<Person> participantsList = new List<Person>();


        public int MaxParticipators
        {
            get { return _maxParticipators; }
            set { this._maxParticipators = value; }
        }

        public DateTime dateTime
        {
            get {return this._dateTime; }
            set { this._dateTime = value;}
        }
        public string Title
        {
            get;
            set;
        }

        public Person Invitor
        {
            get; set;
           
        }
        //public Event(string Title, Person Invitor, List<Person> Participants)
        //{
        //    Invitor = invitor;
        //    Title = title;
        //    this._dateTime = dateTime;

        //}

        public Event(Person invitor, string title, DateTime dateTime, int maxParticipators)
        {
            Invitor = invitor;
            Title = title;
            this._dateTime = dateTime;
            this._maxParticipators = maxParticipators;
        }



        public List<Person> GetParticipants()
        {
            return participantsList;
        }

        public int Compare(Event x, Event y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.dateTime.CompareTo(y.dateTime);
        }

        public void AddPerson(Person p)
        {
            if (!participantsList.Contains(p))
            {
                p.ParticipatingNrOfEv++;
                participantsList.Add(p);
               
            }
        }

        public void RemovePerson(Person p)
        {

            if (participantsList.Contains(p))
            {
                p.ParticipatingNrOfEv--;
                participantsList.Remove(p);
              
            }

        }

        



    }

}
