use SocialDb

select* from LikePosts Order by Timestamp
select* from Comments Order by Timestamp
select* from Friends
select* from Chats
select* from ChatRooms
select* from Users
select* from Posts
select* from Attachments
select* from Groups
select* from GroupMembers


DELETE FROM LikePosts WHERE PostId=2 and UserId = 8
DELETE FROM Friends WHERE FriendId=8 and UserId = 8
DELETE FROM Friends WHERE FriendId=1 and UserId = 8
DELETE FROM Friends WHERE FriendId=8 and UserId = 1

