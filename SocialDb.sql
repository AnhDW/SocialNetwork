use SocialDb

select* from LikePosts Order by Timestamp
select* from Comments Order by Timestamp
select* from Friends
select* from Chats
select* from Rooms
select* from RoomMembers
select* from RoomChats
select* from Users
select* from Posts
select* from Attachments
select* from Groups
select* from GroupMembers


DELETE FROM LikePosts WHERE PostId=2 and UserId = 8
DELETE FROM Friends WHERE FriendId=8 and UserId = 8
DELETE FROM Friends WHERE FriendId=1 and UserId = 6
DELETE FROM Friends WHERE FriendId=6 and UserId = 1

ALTER TABLE GroupMembers NOCHECK CONSTRAINT FK_GroupMembers_Users_MemberId
ALTER TABLE GroupMembers CHECK CONSTRAINT FK_GroupMembers_Users_MemberId
DELETE FROM Groups WHERE Id = 1
DELETE FROM GroupMembers WHERE MemberId = 1 and GroupId = 1

