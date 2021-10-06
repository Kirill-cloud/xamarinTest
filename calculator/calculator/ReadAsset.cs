using Android.App;
using Android.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace calculator
{
    public class ReadAsset : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Image = Assets.Open("math.jpg");
        }
        public Stream Image { get; set; }
    }
}
