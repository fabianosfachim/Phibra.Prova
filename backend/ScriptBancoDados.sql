USE [bd_prova]
GO
/****** Object:  Table [dbo].[movimentacao]    Script Date: 5/31/2023 5:09:03 PM ******/
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
/****** Object:  Table [dbo].[saldomovimentacao]    Script Date: 5/31/2023 5:09:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[saldomovimentacao](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[saldoDespesa] [decimal](18, 2) NOT NULL,
	[saldoReceita] [decimal](18, 2) NOT NULL,
	[saldoTotal] [decimal](18, 2) NOT NULL,
	[dt_movimentacao] [datetime] NOT NULL,
 CONSTRAINT [PK_saldomovimentacao] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipomovimentacao]    Script Date: 5/31/2023 5:09:03 PM ******/
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
SET IDENTITY_INSERT [dbo].[movimentacao] ON 
GO
INSERT [dbo].[movimentacao] ([id], [id_movimentacao], [dt_movimento], [dt_lancamento], [vl_lancamento]) VALUES (1, 1, CAST(N'2023-05-31T19:48:44.643' AS DateTime), CAST(N'2023-05-31T19:48:44.643' AS DateTime), CAST(200.00 AS Decimal(19, 2)))
GO
SET IDENTITY_INSERT [dbo].[movimentacao] OFF
GO
SET IDENTITY_INSERT [dbo].[tipomovimentacao] ON 
GO
INSERT [dbo].[tipomovimentacao] ([id_movimentacao], [tp_movimentacao], [desc_movimentacao]) VALUES (1, N'R', N'Receita')
GO
INSERT [dbo].[tipomovimentacao] ([id_movimentacao], [tp_movimentacao], [desc_movimentacao]) VALUES (2, N'D', N'Despesa')
GO
SET IDENTITY_INSERT [dbo].[tipomovimentacao] OFF
GO
ALTER TABLE [dbo].[saldomovimentacao] ADD  CONSTRAINT [DF_saldomovimentacao_dt_movimentacao]  DEFAULT (getdate()) FOR [dt_movimentacao]
GO
ALTER TABLE [dbo].[movimentacao]  WITH CHECK ADD  CONSTRAINT [FK_movimentacao_tipomovimentacao] FOREIGN KEY([id_movimentacao])
REFERENCES [dbo].[tipomovimentacao] ([id_movimentacao])
GO
ALTER TABLE [dbo].[movimentacao] CHECK CONSTRAINT [FK_movimentacao_tipomovimentacao]
GO
