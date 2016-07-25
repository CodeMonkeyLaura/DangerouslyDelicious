using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DangerouslyDelicious.Dtos;
using DangerouslyDelicious.Utilities;

namespace DangerouslyDelicious.Adapters
{
    public class SearchListAdapter : BaseAdapter<YelpListingDto>
    {
        private readonly List<YelpListingDto> _items;
        private readonly Activity _context;

        public SearchListAdapter(Activity context, List<YelpListingDto> items)
        {
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override YelpListingDto this[int position]
        {
            get { return _items[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.SearchList, null);

            view.FindViewById<TextView>(Resource.Id.searchRestaurantName).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.searchRestaurantAddress).Text = item.Address;
            view.FindViewById<TextView>(Resource.Id.searchNumberReviews).Text = $"{item.NumberReviews} Reviews";
            view.FindViewById<ImageView>(Resource.Id.searchYelpStars).SetImageBitmap(MakeBitmap.GetRatingStars(item.RatingImage));

            return view;
        }
    }
}