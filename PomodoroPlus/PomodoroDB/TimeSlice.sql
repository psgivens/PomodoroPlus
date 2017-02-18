CREATE TABLE [dbo].[TimeSlice](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[Duration] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_dbo.TimeSlice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]