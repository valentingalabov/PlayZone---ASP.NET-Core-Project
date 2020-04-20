namespace PlayZone.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PlayZone.Data.Common.Repositories;
    using PlayZone.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetDownVotes(string videoId)
        {
            var downVotes = this.votesRepository.All()
               .Where(x => x.VideoId == videoId && (int)x.Type == -1).Sum(x => (int)x.Type);

            return Math.Abs(downVotes);
        }

        public int GetUpVotes(string videoId)
        {
            var upVotes = this.votesRepository.All()
                .Where(x => x.VideoId == videoId && (int)x.Type == 1).Sum(x => (int)x.Type);

            return upVotes;
        }

        public async Task VoteAsync(string videoId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.VideoId == videoId && x.UserId == userId);
            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    VideoId = videoId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
