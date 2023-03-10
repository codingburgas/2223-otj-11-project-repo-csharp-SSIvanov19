using Microsoft.TeamsFx.Configuration;

namespace CBCanteen.Client.Teams;

public class ConfigOptions
{
    public TeamsFxOptions TeamsFx { get; set; }
}

public class TeamsFxOptions
{
    public AuthenticationOptions Authentication { get; set; }
}
