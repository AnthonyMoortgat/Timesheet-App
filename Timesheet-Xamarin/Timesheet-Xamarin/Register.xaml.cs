using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Timesheet_Library.Dto;
using Timesheet_Library.Dto.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timesheet_Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Register : ContentPage
	{
		public Register ()
		{
			InitializeComponent ();
		}

        private async void ClickRegister(object sender, EventArgs e)
        {
            List<Entry> NoErrors = new List<Entry>();

            List<Entry> Errors = new List<Entry>();

            //first name
            if (CheckStringIsFilled(firstname.Text))
            {
                Errors.Add(firstname);
                firstnameError.Text = "This field is required!";
                firstnameError.IsVisible = true;
            }
            else
            {
                NoErrors.Add(firstname);
                firstnameError.IsVisible = false;
            }

            //last name
            if (CheckStringIsFilled(lastname.Text))
            {
                Errors.Add(lastname);
                lastnameError.Text = "This field is required!";
                lastnameError.IsVisible = true;
            }
            else
            {
                NoErrors.Add(lastname);
                lastnameError.IsVisible = false;
            }

            //email
            if (CheckStringIsFilled(email.Text))
            {
                Errors.Add(email);
                emailError.Text = "This field is required!";
                emailError.IsVisible = true;
            }
            else
            {
                if (CheckEmail(email.Text))
                {
                    NoErrors.Add(email);
                    emailError.IsVisible = false;
                }
                else
                {
                    Errors.Add(email);
                    emailError.Text = "E-mail is not correct!";
                    emailError.IsVisible = true;
                }
            }

            //phone number
            if (CheckStringIsFilled(phonenumber.Text))
            {
                Errors.Add(phonenumber);
                phoneError.Text = "This field is required!";
                phoneError.IsVisible = true;
            }
            else
            {
                if (CheckPhone(phonenumber.Text))
                {
                    NoErrors.Add(phonenumber);
                    phoneError.IsVisible = false;
                }
                else
                {
                    Errors.Add(phonenumber);
                    phoneError.Text = "Phone number is not correct!";
                    phoneError.IsVisible = true;
                }
            }

            //Street name
            if (CheckStringIsFilled(streetname.Text))
            {
                Errors.Add(streetname);
                streetnameError.Text = "This field is required!";
                streetnameError.IsVisible = true;
            }
            else
            {
                NoErrors.Add(streetname);
                streetnameError.IsVisible = false;
            }

            //House number
            if (CheckStringIsFilled(housenumber.Text))
            {
                Errors.Add(housenumber);
                housenumberError.Text = "This field is required!";
                housenumberError.IsVisible = true;
            }
            else
            {
                NoErrors.Add(housenumber);
                housenumberError.IsVisible = false;
            }

            //Country
            if (CheckStringIsFilled(country.Text))
            {
                Errors.Add(country);
                countryError.Text = "This field is required!";
                countryError.IsVisible = true;
            }
            else
            {
                NoErrors.Add(country);
                countryError.IsVisible = false;
            }

            //Postal code
            if (CheckStringIsFilled(postalcode.Text))
            {
                Errors.Add(postalcode);
                postalcodeError.Text = "This field is required!";
                postalcodeError.IsVisible = true;
            }
            else
            {
                if (CheckPostalCode(postalcode.Text))
                {
                    NoErrors.Add(postalcode);
                    postalcodeError.IsVisible = false;
                }
                else
                {
                    Errors.Add(postalcode);
                    postalcodeError.Text = "Postal code is not correct!";
                    postalcodeError.IsVisible = true;
                }
            }

            //City
            if (CheckStringIsFilled(city.Text))
            {
                Errors.Add(city);
                cityError.Text = "This field is required!";
                cityError.IsVisible = true;
            }
            else
            {
                NoErrors.Add(city);
                cityError.IsVisible = false;
            }

            //Password
            if (CheckStringIsFilled(password.Text))
            {
                Errors.Add(password);
                passwordError.Text = "This field is required!";
                passwordError.IsVisible = true;
            }
            else
            {
                if (CheckPassword(password.Text))
                {
                    NoErrors.Add(password);
                    passwordError.IsVisible = false;
                }
                else
                {
                    Errors.Add(password);
                    passwordError.Text = "Password must between 8 and 20 characters,and must contain atleast one digit, atleast one upper case and atleast one lower case and no whitespace!";
                    passwordError.IsVisible = true;
                }
            }

            //Confirm password
            if (CheckpasswordEquality(password.Text, confirmpassword.Text))
            {
                NoErrors.Add(confirmpassword);
                confirmpasswordError.IsVisible = false;
            }
            else
            {
                Errors.Add(confirmpassword);
                confirmpasswordError.Text = "The passwords are not equal!";
                confirmpasswordError.IsVisible = true;
            }

            /*
            if (Errors.Count == 0)
            {
                await DisplayAlert("Message", $"No errors found! Errors: {Errors.Count}", "Ok");
            }
            */

            if (Errors.Count() == 0)
            {
                UserServices userServices = new UserServices();

                UserToCreateDto user = new UserToCreateDto
                {
                    Email = email.Text,
                    Psw = password.Text,
                    FirstName = firstname.Text,
                    LastName = lastname.Text,
                    PhoneNumber = phonenumber.Text,
                    Address = new AddressToCreateDto
                    {
                        Street = streetname.Text,
                        HouseNumber = housenumber.Text,
                        Country = country.Text,
                        PostalCode = postalcode.Text,
                        City = city.Text,
                        BoxNumber = boxnumber.Text
                    }
                };
                buttonRegister.IsEnabled = false;
                //await DisplayAlert("Warning", Errors.Count() + " haha", "Ok");
                await userServices.CreateUserAsync(user);
                
                Application.Current.MainPage = new Login();
            }

            SetError(Errors);
            SetNoError(NoErrors);
        }

        private void SetError(List<Entry> Errors)
        {
            for (int i = 0; i < Errors.Count; i++)
            {
                Errors[i].BackgroundColor = Xamarin.Forms.Color.Red;
                Errors[i].TextColor = Xamarin.Forms.Color.White;
            }
            Errors.Clear();
        }

        private void SetNoError(List<Entry> NoErrors)
        {
            for (int i = 0; i < NoErrors.Count; i++)
            {
                NoErrors[i].BackgroundColor = Xamarin.Forms.Color.GhostWhite;
                NoErrors[i].TextColor = Xamarin.Forms.Color.Black;
            }
            NoErrors.Clear();
        }

        private void GoToLoginPage(object sender, EventArgs args)
        {
            Application.Current.MainPage = new Login();
        }

        //Check op lege string
        private static Predicate<string> CheckStringIsFilled = str => str.Trim() == "";

        //Email controle
        private static Predicate<string> CheckEmail = email => Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        //Check op phone number
        private static Predicate<string> CheckPhone = phone => Regex.IsMatch(phone, @"^([0-9]{9,10})$");

        //Password controle: tussen 8 en 20 characters max en moet uit minstens 1 hoofdletter, 1 speciaal teken en 1 nummer bestaan
        private static Predicate<string> CheckPassword = pass => Regex.IsMatch(pass, @"(?=^.{8,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$");

        //Postalcode check: tussen 1000 en 9999
        private static Predicate<string> CheckPostalCode = postal => Regex.IsMatch(postal, @"^([1-9][0-9][0-9][0-9])$");

        //Check dat beide password gelijk zijn aan elkaar
        private static bool CheckpasswordEquality(string Password, string CheckPassword)
        {
            if (Password == CheckPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

// Stackoverflow. Regular expression for password validation
// https://stackoverflow.com/questions/5859632/regular-expression-for-password-validation
// Geraadpleegd op 18 maart 2019