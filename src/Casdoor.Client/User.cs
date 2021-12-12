namespace Casdoor;

public class User
{
    public string? Owner { get; set; }
    public string? Name { get; set; }
    public string? CreatedTime { get; set; }
    public string? UpdatedTime { get; set; }
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public string? DisplayName { get; set; }
    public string? Avatar { get; set; }
    public string? PermanentAvatar { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public IEnumerable<string>? Address { get; set; }
    public string? Affiliation { get; set; }
    public string? Title { get; set; }
    public string? IdCardType { get; set; }
    public string? IdCard { get; set; }
    public string? Homepage { get; set; }
    public string? Bio { get; set; }
    public string? Tag { get; set; }
    public string? Region { get; set; }
    public string? Language { get; set; }
    public string? Gender { get; set; }
    public string? Birthday { get; set; }
    public string? Education { get; set; }
    public int Score { get; set; }
    public int Ranking { get; set; }
    public bool IsDefaultAvatar { get; set; }
    public bool IsOnline { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsGlobalAdmin { get; set; }
    public bool IsForbidden { get; set; }
    public bool IsDeleted { get; set; }
    public string? SignupApplication { get; set; }
    public string? Hash { get; set; }
    public string? PreHash { get; set; }
    public string? CreatedIp { get; set; }
    public string? LastSigninTime { get; set; }
    public string? LastSigninIp { get; set; }
    public string? Github { get; set; }
    public string? Google { get; set; }
    public string? Qq { get; set; }
    public string? Wechat { get; set; }
    public string? Facebook { get; set; }
    public string? Dingtalk { get; set; }
    public string? Weibo { get; set; }
    public string? Gitee { get; set; }
    public string? Linkedin { get; set; }
    public string? Wecom { get; set; }
    public string? Lark { get; set; }
    public string? Gitlab { get; set; }
    public string? Ldap { get; set; }
    public IDictionary<string, string>? Properties { get; set; }
}