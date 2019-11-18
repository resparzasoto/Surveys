using Surveys.Entities;
using System.IO;
using Xamarin.Forms;

namespace Surveys.ViewModels
{
    public class TeamViewModel : ViewModelBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                if (id == value)
                {
                    return;
                }
                id = value;
                RaisePropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                {
                    return;
                }
                name = value;
                RaisePropertyChanged();
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                if (color == value)
                {
                    return;
                }
                color = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource logo;

        public ImageSource Logo
        {
            get { return logo; }
            set
            {
                if (logo == value)
                {
                    return;
                }
                logo = value;
                RaisePropertyChanged();
            }
        }

        public static TeamViewModel GetViewModelFromEntity(Team entity)
        {
            var result = new TeamViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Color = entity.Color,
                Logo = ImageSource.FromStream(() => new MemoryStream(entity.Logo))
            };

            return result;
        }
    }
}
