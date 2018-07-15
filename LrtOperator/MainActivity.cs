using Android.App;
using Android.Locations;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using LrtOperator.Model;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Threading.Tasks;

namespace LrtOperator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {

        FragmentTransaction ft;

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:

                    FragmentManager.BeginTransaction().Remove(FragmentManager.FindFragmentById(Resource.Id.FrameLayout)).Commit();
                    ft = FragmentManager.BeginTransaction();
                    ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
                    ft.AddToBackStack(null);
                    ft.Add(Resource.Id.FrameLayout, new HomeFragment());
                    ft.Commit();

                    return true;


                case Resource.Id.navigation_notifications:

                    FragmentManager.BeginTransaction().Remove(FragmentManager.FindFragmentById(Resource.Id.FrameLayout)).Commit();
                    ft = FragmentManager.BeginTransaction();
                    ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
                    ft.AddToBackStack(null);
                    ft.Add(Resource.Id.FrameLayout, new NotificationFragment());
                    ft.Commit();

                    return true;
            }
            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);


            ft = FragmentManager.BeginTransaction();
            ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
            ft.AddToBackStack(null);
            ft.Add(Resource.Id.FrameLayout, new HomeFragment());
            ft.Commit();

        }

        #region doubleback press to exit app
        bool doubleBackToExitPressedOnce = false;

        public override void OnBackPressed()
        {

            if (doubleBackToExitPressedOnce)
            {
                base.OnBackPressed();
                Finish();
                return;
            }


            this.doubleBackToExitPressedOnce = true;

            Toast.MakeText(this, "Tap back twice to exit", ToastLength.Short).Show();
            new Handler().PostDelayed(() =>
            {
                doubleBackToExitPressedOnce = false;
            }, 2000);


        }
        #endregion
    }
}

