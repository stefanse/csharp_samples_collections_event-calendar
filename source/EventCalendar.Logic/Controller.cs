using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using EventCalendar.Entities;
using static System.String;

namespace EventCalendar.Logic
{
    public class Controller: List<Event>, IComparer<Event>
    {
        private readonly ICollection<Event> _events; 
        public List<Event> P_Prtpts_E; // person participates Event
        public int EventsCount { get { return _events.Count; } }

        public Controller()
        {
            _events = new List<Event>();
        }

        /// <summary>
        /// Ein Event mit dem angegebenen Titel und dem Termin wird für den Einlader angelegt.
        /// Der Titel muss innerhalb der Veranstaltungen eindeutig sein und das Datum darf nicht
        /// in der Vergangenheit liegen.
        /// Mit dem optionalen Parameter maxParticipators kann eine Obergrenze für die Teilnehmer festgelegt
        /// werden.
        /// </summary>
        /// <param name="invitor"></param>
        /// <param name="title"></param>
        /// <param name="dateTime"></param>
        /// <param name="maxParticipators"></param>
        /// <returns>Wurde die Veranstaltung angelegt</returns>
        public bool CreateEvent(Person invitor, string title, DateTime dateTime, int maxParticipators = 0)
        {
            
            foreach (Event e in _events)
            {
                if (title.Equals(e.Title))
                    return false;
            }

            if (dateTime > DateTime.Now && !(title.Equals(null)) &&  title != "" && invitor != null && dateTime != null && maxParticipators >= 0 && invitor != null )
            {
                _events.Add(new Event(invitor, title, dateTime, maxParticipators));
                return true;
            }

            //if(maxParticipators == 0)
            //{
            //    Event a = new Event(invitor, title, dateTime);
            //    _events.Add(a );
            //}
            
            return false;
        }


        /// <summary>
        /// Liefert die Veranstaltung mit dem Titel
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Event oder null, falls es keine Veranstaltung mit dem Titel gibt</returns>
        public Event GetEvent(string title)
        {

            foreach (Event e in _events)
            {
                if (e.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase))
                {
                    return e;
                }
         
            }
            return null;


            //throw new NotImplementedException();
        }

        /// <summary>
        /// Person registriert sich für Veranstaltung.
        /// Eine Person kann sich zu einer Veranstaltung nur einmal registrieren.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="ev">Veranstaltung</param>
        /// <returns>War die Registrierung erfolgreich?</returns>
        public bool RegisterPersonForEvent(Person person, Event ev)
        {
           
            foreach (Event e in _events)
            {
                foreach (Person p in ev.participantsList)
                {
                    if (p.LastName.Equals(person.LastName) && p.FirstName.Equals(person.FirstName))
                    {
                        return false;
                    }
                }
            }
            if (person != null && ev != null)
            {

                if (ev.MaxParticipators <= ev.participantsList.Count)
                {
                    // ev.participantsList.Add(person);
                    ev.AddPerson(person);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Person meldet sich von Veranstaltung ab
        /// </summary>
        /// <param name="person"></param>
        /// <param name="ev">Veranstaltung</param>
        /// <returns>War die Abmeldung erfolgreich?</returns>
        public bool UnregisterPersonForEvent(Person person, Event ev)
        {
          
            
            if (person != null  && ev != null && ev.Title != null )
            {
                //ev.participantsList.Remove(person);
                ev.RemovePerson(person);
                return true;
            }
                
            
            return false;
        }

        /// <summary>
        /// Liefert alle Teilnehmer an der Veranstaltung.
        /// Sortierung absteigend nach der Anzahl der Events der Personen.
        /// Bei gleicher Anzahl nach dem Namen der Person (aufsteigend).
        /// </summary>
        /// <param name="ev"></param>
        /// <returns>Liste der Teilnehmer oder null im Fehlerfall</returns>
        public IList<Person> GetParticipatorsForEvent(Event ev)
        {
            List<Person> eventParticipantsList = null;
          
            if (ev != null && ev.participantsList != null)
            {
                eventParticipantsList = ev.GetParticipants();
                eventParticipantsList.Sort();
                return eventParticipantsList;
            }
          
                return null;
            
        }

        /// <summary>
        /// Liefert alle Veranstaltungen der Person nach Datum (aufsteigend) sortiert.
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Liste der Veranstaltungen oder null im Fehlerfall</returns>
        public List<Event> GetEventsForPerson(Person person)
        {
            P_Prtpts_E = new List<Event>();
            foreach (Event  e in _events)
            {
                //if (person.Equals(e.participantsList) )
                //{
                //    P_Prtpts_E.Add(e);
                //}
                if (e.participantsList.Contains(person))
                {
                    P_Prtpts_E.Add(e);
                }

            }
            if (P_Prtpts_E.Count != 0 && person != null)
            {
                //P_Prtpts_E.Sort();
                return P_Prtpts_E;
            }
            else
            {
                return null;
            }

        }

        

        /// <summary>
        /// Liefert die Anzahl der Veranstaltungen, für die die Person registriert ist.
        /// </summary>
        /// <param name="participator"></param>
        /// <returns>Anzahl oder 0 im Fehlerfall</returns>
        public int CountEventsForPerson(Person participator)
        {
            int counter = 0;
            foreach(Event e in _events)
            {
                foreach (Person p in e.participantsList) 
                {
                    if (participator.Equals(p))
                    {
                        counter++;
                    }
                }
              
            }
            return counter;
        }

        public int Compare(Event ev1, Event ev2) // sorting date asc.
        {
            if(ev1 == null && ev2 == null)
            {
                return 0;
            }
            
            if(ev1 == null)
            {
                return -1;
            }
            if(ev2 == null)
            {
                return 1;
            }
            return ev1.dateTime.CompareTo(ev2.dateTime);


        }
        
        

   

    }
}
