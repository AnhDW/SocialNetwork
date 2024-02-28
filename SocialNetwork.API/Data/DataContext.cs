using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends {  get; set; } 
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }  
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomMember> RoomMembers { get; set; }
        public DbSet<RoomChat> RoomChats { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TokenManagement> TokenManagements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>().HasKey(k => new { k.UserId, k.FriendId });
            modelBuilder.Entity<Friend>().HasOne(c => c.CurrentUser).WithMany(c => c.ConnectedUsers)
                .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Friend>().HasOne(c=>c.ConnectedUser).WithMany(c => c.CurrentUsers)
                .HasForeignKey(u=>u.FriendId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>().HasMany(p => p.Posts).WithOne(u => u.User)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<User>().HasMany(p => p.Notifications).WithOne(u => u.User)
                .HasForeignKey(g => g.UserId);
            modelBuilder.Entity<User>().HasMany(p => p.Groups).WithOne(u => u.User)
                .HasForeignKey(g => g.CreatorId);
            modelBuilder.Entity<User>().HasMany(p => p.Rooms).WithOne(u => u.User)
                .HasForeignKey(g => g.CreatorId);
            modelBuilder.Entity<User>().HasMany(p => p.Events).WithOne(u => u.User)
                .HasForeignKey(g => g.CreatorId);
            modelBuilder.Entity<User>().HasMany(p => p.TokenManagements).WithOne(u => u.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Attachment>().HasOne(p => p.Post).WithMany(a => a.Attachments)
                .HasForeignKey(a => a.PostId);

            modelBuilder.Entity<Comment>().HasKey(k => new { k.UserId, k.PostId, k.Timestamp });
            modelBuilder.Entity<Comment>().HasOne(u => u.User).WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasOne(u => u.Post).WithMany(c => c.Comments)
                .HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LikePost>().HasKey(k => new { k.UserId, k.PostId });
            modelBuilder.Entity<LikePost>().HasOne(u => u.User).WithMany(c => c.LikePosts)
                .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LikePost>().HasOne(u => u.Post).WithMany(c => c.LikePosts)
                .HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Chat>().HasOne(s => s.Sender).WithMany(s => s.SenderList)
                .HasForeignKey(c => c.SenderId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Chat>().HasOne(s => s.Receiver).WithMany(s => s.RecipientList)
                .HasForeignKey(c => c.ReceiverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupMember>().HasKey(k => new { k.GroupId, k.MemberId });
            modelBuilder.Entity<GroupMember>().HasOne(g => g.Group).WithMany(g => g.GroupMembers)
                .HasForeignKey(g => g.GroupId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<GroupMember>().HasOne(g => g.Member).WithMany(g => g.GroupMembers)
                .HasForeignKey(g => g.MemberId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RoomMember>().HasKey(k => new { k.RoomId, k.MemberId });
            modelBuilder.Entity<RoomMember>().HasOne(r => r.Room).WithMany(r => r.RoomMembers)
                .HasForeignKey(r => r.RoomId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RoomMember>().HasOne(r => r.Member).WithMany(r => r.RoomMembers)
                .HasForeignKey(r => r.MemberId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RoomChat>().HasOne(r => r.Room).WithMany(r => r.ChatRooms)
                .HasForeignKey(r => r.RoomId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RoomChat>().HasOne(r => r.User).WithMany(r => r.ChatRooms)
                .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
