using System;

namespace hw2
{

    public class Address : IComparable<Address>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLineOne { get; set; }

        public string AddressLineTwo { get; set; }

        public string City { get; set; }

        public int Zip { get; set; }

        public string State { get; set; }

        public DateTime BirthDay { get; set; }

        public string PhoneNumber { get; set; }
        // constructor Address, setting values;
        public Address(string firstName, string lastName, string addressLineOne, string addressLineTwo, string city, string state, int zip, DateTime birthDay, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            City = city;
            State = state;
            Zip = zip;
            BirthDay = birthDay;
            PhoneNumber = phoneNumber;
        }

        public int CompareTo(Address other)
        {
            // constant values with descriptive names;
            const int EQUAL = 0;
            const int PRECEDES = -1;
            const int FOLLOWS = 1;

            if (this.Equals(other))
            {
                return EQUAL;
            }
            // checking for precedences
            if (LastName.CompareTo(other.LastName) < 0)
            {
                return PRECEDES;
            }
            else if (LastName.CompareTo(other.LastName) == 0)
            {
                if (FirstName.CompareTo(other.FirstName) < 0)
                {
                    return PRECEDES;
                }
                else if (FirstName.CompareTo(other.FirstName) == 0)
                {
                    if (Zip - other.Zip < 0)
                    {
                        return PRECEDES;
                    }

                    return FOLLOWS;

                }

                return FOLLOWS;

            }
            return FOLLOWS;
        }
        //overriding tostring method to follow the standart of the homework
        public override string ToString()
        {
            return $"{FirstName} {LastName}\n{AddressLineOne}\n{AddressLineTwo}\n{City}, {State}, {Zip}\n{PhoneNumber}";
        }

        public override bool Equals(object obj)

        {
            // since null is a type of object i check whether or not i get an actual object or not
            if (obj == null)
            {
                return false;
            }

            // checking if the type my current instance is the same as the parameter's
            if (GetType() != obj.GetType())
            {
                return false;
            }

            // saving obj in some other variable in order to make the code readable
            // using as keyword to convert my obj to Address type
            Address inputObject = obj as Address;

            if (LastName.CompareTo(inputObject.LastName) == 0)
            {
                if (FirstName.CompareTo(inputObject.FirstName) == 0)
                {
                    if (Zip - inputObject.Zip == 0)
                    {

                        return true;
                    }

                }
            }

            return false;

        }

        public override int GetHashCode()
        {
            // if the two strings are the same they will have the same hashcode
            return Zip * FirstName.GetHashCode() * LastName.GetHashCode();
        }
    }
}
