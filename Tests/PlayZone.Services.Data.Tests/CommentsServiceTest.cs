namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using Xunit;

    public class CommentsServiceTest
    {
        private readonly EfDeletableEntityRepository<Comment> commentRepository;
        private readonly CommentsService service;
        private readonly Comment comment1;
        private readonly Comment comment2;

        public CommentsServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            this.commentRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));

            this.service = new CommentsService(this.commentRepository);

            this.comment1 = new Comment
            {
                Id = 1,
                Content = "FirstComment",
                UserId = null,
                VideoId = "videoId",
            };

            this.comment2 = new Comment
            {
                Id = 2,
                Content = "SecondComment",
                UserId = null,
                VideoId = "videoId",
            };
        }

        [Fact]
        public async Task CreateCommentTest()
        {
            await this.service.Create("videoId", "userId", "Content");

            Assert.Single(this.commentRepository.All());
        }

        [Fact]
        public async Task CreateCommentDefaultParrentIdTest()
        {
            await this.service.Create("videoId", "userId", "Content");

            var parentId = this.commentRepository.All().Where(c => c.VideoId == "videoId").Select(c => c.ParentId).First();

            Assert.Null(parentId);
        }

        [Fact]
        public async Task CreateCommentParentIdSetCorrectTest()
        {
            await this.service.Create("videoId", "userId", "Content", 4);

            var parentId = this.commentRepository.All().Where(c => c.VideoId == "videoId").Select(c => c.ParentId).First();

            Assert.Equal(4, parentId);
        }

        [Fact]
        public async Task IsInVideoIdReturnTrueCorrectTest()
        {
            await this.AddCommentsToRepository();

            Assert.True(this.service.IsInVideoId(this.comment1.Id, this.comment1.VideoId));
        }

        [Fact]
        public async Task IsInVideoIdReturnFalseCorrectTest()
        {
            await this.AddCommentsToRepository();

            Assert.False(this.service.IsInVideoId(this.comment1.Id, "testVideoId"));
        }

        private async Task AddCommentsToRepository()
        {
            await this.commentRepository.AddAsync(this.comment1);
            await this.commentRepository.AddAsync(this.comment2);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
