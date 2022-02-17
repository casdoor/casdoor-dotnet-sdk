using Newtonsoft.Json;
using SqlSugar;

namespace Casdoor.Client;

/// <summary>
/// CasdoorUser has the same definition as https://github.com/casdoor/casdoor/blob/master/object/user.go#L24
/// </summary>
public class CasdoorUser
{
    // TODO: test for xml, json: marshal & unmarshal
    // TODO: export NuGet dependencies: SqlSugar >= 5.0.5.4; Newtonsoft.Json >= 13.0.1

    [SugarColumn(ColumnDataType = "varchar(100)", IsNullable = false, IsPrimaryKey = true), JsonProperty("owner")]
    public string? Owner { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)", IsNullable = false, IsPrimaryKey = true), JsonProperty("name")]
    public string? Name { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("createdTime")]
    public string? CreatedTime { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("updatedTime")]
    public string? UpdatedTime { get; set; }


    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("id")]
    public string? Id { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("type")]
    public string? Type { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("password")]
    public string? Password { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("passwordSalt")]
    public string? PasswordSalt { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("avatar")]
    public string? Avatar { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("permanentAvatar")]
    public string? PermanentAvatar { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("email")]
    public string? Email { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("phone")]
    public string? Phone { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("location")]
    public string? Location { get; set; }

    [JsonProperty("address")]
    public IEnumerable<string>? Address { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("affiliation")]
    public string? Affiliation { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("title")]
    public string? Title { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("idCardType")]
    public string? IdCardType { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("idCard")]
    public string? IdCard { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("homePage")]
    public string? Homepage { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("bio")]
    public string? Bio { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("tag")]
    public string? Tag { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("region")]
    public string? Region { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("language")]
    public string? Language { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("gender")]
    public string? Gender { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("birthday")]
    public string? Birthday { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("education")]
    public string? Education { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }

    [JsonProperty("ranking")]
    public int Ranking { get; set; }

    [JsonProperty("isDefaultAvatar")]
    public bool IsDefaultAvatar { get; set; }

    [JsonProperty("isOnline")]
    public bool IsOnline { get; set; }

    [JsonProperty("isAdmin")]
    public bool IsAdmin { get; set; }

    [JsonProperty("isGlobalAdmin")]
    public bool IsGlobalAdmin { get; set; }

    [JsonProperty("isForbidden")]
    public bool IsForbidden { get; set; }

    [JsonProperty("isDeleted")]
    public bool IsDeleted { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("signupApplication")]
    public string? SignupApplication { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("hash")]
    public string? Hash { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("preHash")]
    public string? PreHash { get; set; }


    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("createdIp")]
    public string? CreatedIp { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("lastSigninTime")]
    public string? LastSigninTime { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("lastSigninIp")]
    public string? LastSigninIp { get; set; }


    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("github")]
    public string? Github { get; set; }

    [SugarColumn(ColumnDataType = "varchar(100)"), JsonProperty("google")]
    public string? Google { get; set; }

    [SugarColumn(ColumnName = "qq", ColumnDataType = "varchar(100)"), JsonProperty("qq")]
    public string? QQ { get; set; }

    [SugarColumn(ColumnName = "wechat", ColumnDataType = "varchar(100)"), JsonProperty("wechat")]
    public string? WeChat { get; set; }

    [SugarColumn(ColumnName = "facebook", ColumnDataType = "varchar(100)"), JsonProperty("facebook")]
    public string? Facebook { get; set; }

    [SugarColumn(ColumnName = "dingtalk", ColumnDataType = "varchar(100)"), JsonProperty("dingtalk")]
    public string? DingTalk { get; set; }

    [SugarColumn(ColumnName = "weibo", ColumnDataType = "varchar(100)"), JsonProperty("weibo")]
    public string? Weibo { get; set; }

    [SugarColumn(ColumnName = "gitee", ColumnDataType = "varchar(100)"), JsonProperty("gitee")]
    public string? Gitee { get; set; }

    [SugarColumn(ColumnName = "linkedin", ColumnDataType = "varchar(100)"), JsonProperty("linkedin")]
    public string? LinkedIn { get; set; }

    [SugarColumn(ColumnName = "wecom", ColumnDataType = "varchar(100)"), JsonProperty("wecom")]
    public string? Wecom { get; set; }

    [SugarColumn(ColumnName = "lark", ColumnDataType = "varchar(100)"), JsonProperty("lark")]
    public string? Lark { get; set; }

    [SugarColumn(ColumnName = "gitlab", ColumnDataType = "varchar(100)"), JsonProperty("gitlab")]
    public string? Gitlab { get; set; }


    [SugarColumn(ColumnName = "ldap", ColumnDataType = "varchar(100)"), JsonProperty("ldap")]
    public string? Ldap { get; set; }

    [JsonProperty("properties")]
    public IDictionary<string, string>? Properties { get; set; }
}
