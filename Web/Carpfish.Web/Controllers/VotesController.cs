namespace Carpfish.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<PostLakeVoteResponseModel>> PostLakeVote(PostLakeVoteInputModel inputModel)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetLakeVoteAsync(inputModel.LakeId, userId, inputModel.Value);

            return new PostLakeVoteResponseModel
            {
                AverageVote = this.votesService.GetLakeAverageVote(inputModel.LakeId),
                RatersCount = this.votesService.GetLakeRatersCount(inputModel.LakeId),
                OneStarRatersCount = this.votesService.GetLakeRatersCountByValue(inputModel.LakeId, 1),
                TwoStarRatersCount = this.votesService.GetLakeRatersCountByValue(inputModel.LakeId, 2),
                ThreeStarRatersCount = this.votesService.GetLakeRatersCountByValue(inputModel.LakeId, 3),
                FourStarRatersCount = this.votesService.GetLakeRatersCountByValue(inputModel.LakeId, 4),
                FiveStarRatersCount = this.votesService.GetLakeRatersCountByValue(inputModel.LakeId, 5),
            };
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<PostTrophyVoteResponseModel>> PostTrophyVote(PostTrophyVoteInputModel inputModel)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetTrophyVoteAsync(inputModel.TrophyId, userId, inputModel.Value);

            return new PostTrophyVoteResponseModel
            {
                AverageVote = this.votesService.GetTrophyAverageVote(inputModel.TrophyId),
                RatersCount = this.votesService.GetTrophyRatersCount(inputModel.TrophyId),
            };
        }
    }
}
