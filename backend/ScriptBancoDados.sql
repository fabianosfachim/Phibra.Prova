USE [bd_prova]
GO
/****** Object:  Table [dbo].[movimentacao]    Script Date: 5/31/2023 4:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimentacao](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_movimentacao] [int] NOT NULL,
	[dt_movimento] [datetime] NOT NULL,
	[dt_lancamento] [datetime] NOT NULL,
	[vl_lancamento] [decimal](19, 2) NOT NULL,
 CONSTRAINT [PK_tb_movimentacao] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipomovimentacao]    Script Date: 5/31/2023 4:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipomovimentacao](
	[id_movimentacao] [int] IDENTITY(1,1) NOT NULL,
	[tp_movimentacao] [char](1) NOT NULL,
	[desc_movimentacao] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tb_tp_movimentacao] PRIMARY KEY CLUSTERED 
(
	[id_movimentacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[movimentacao]  WITH CHECK ADD  CONSTRAINT [FK_movimentacao_tipomovimentacao] FOREIGN KEY([id_movimentacao])
REFERENCES [dbo].[tipomovimentacao] ([id_movimentacao])
GO
ALTER TABLE [dbo].[movimentacao] CHECK CONSTRAINT [FK_movimentacao_tipomovimentacao]
GO
