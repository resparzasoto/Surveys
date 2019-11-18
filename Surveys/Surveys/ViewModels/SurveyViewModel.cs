using Surveys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Surveys.ViewModels
{
    public class SurveyViewModel : ViewModelBase
    {
        public string Id { get; set; }

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

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate == value)
                {
                    return;
                }
                birthDate = value;
                RaisePropertyChanged();
            }
        }

        private TeamViewModel team;

        public TeamViewModel Team
        {
            get { return team; }
            set
            {
                if (team == value)
                {
                    return;
                }
                team = value;
                RaisePropertyChanged();
            }
        }

        private double lat;

        public double Lat
        {
            get { return lat; }
            set
            {
                if (lat == value)
                {
                    return;
                }
                lat = value;
                RaisePropertyChanged();
            }
        }

        private double lon;

        public double Lon
        {
            get { return lon; }
            set
            {
                if (lon == value)
                {
                    return;
                }
                lon = value;
                RaisePropertyChanged();
            }
        }

        public static SurveyViewModel GetViewModelFromEntity(Survey entity, IEnumerable<Team> teams)
        {
            var result = new SurveyViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                BirthDate = entity.Birthdate,
                Team = TeamViewModel.GetViewModelFromEntity(teams.First(t => t.Id == entity.TeamId)),
                Lat = entity.Lat,
                Lon = entity.Lon
            };

            return result;
        }

        public static Survey GetEntityFromViewModel(SurveyViewModel viewModel)
        {
            var result = new Survey
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Birthdate = viewModel.BirthDate,
                TeamId = viewModel.Team.Id,
                Lat = viewModel.Lat,
                Lon = viewModel.Lon
            };

            return result;
        }
    }
}
