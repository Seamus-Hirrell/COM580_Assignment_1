
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/31/2022 19:34:16
-- Generated from EDMX file: C:\Users\sicro\source\repos\assignment_1\VeterinaryPracticeModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [assignment_1.VeterinaryPractice];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [OwnerId] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Pets'
CREATE TABLE [dbo].[Pets] (
    [PetId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Breed] nvarchar(max)  NOT NULL,
    [OwnerOwnerId] int  NOT NULL
);
GO

-- Creating table 'Practices'
CREATE TABLE [dbo].[Practices] (
    [RegNum] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Vets'
CREATE TABLE [dbo].[Vets] (
    [VetId] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PracticeRegNum] int  NOT NULL
);
GO

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [VisitId] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Reason] nvarchar(max)  NOT NULL,
    [PetPetId] int  NOT NULL,
    [VetVetId] int  NOT NULL,
    [Treatment_TreatmentId] int  NOT NULL
);
GO

-- Creating table 'Treatments'
CREATE TABLE [dbo].[Treatments] (
    [TreatmentId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Medications'
CREATE TABLE [dbo].[Medications] (
    [MedicationId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Cost] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VisitMedication'
CREATE TABLE [dbo].[VisitMedication] (
    [Visits_VisitId] int  NOT NULL,
    [Medications_MedicationId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [OwnerId] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([OwnerId] ASC);
GO

-- Creating primary key on [PetId] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [PK_Pets]
    PRIMARY KEY CLUSTERED ([PetId] ASC);
GO

-- Creating primary key on [RegNum] in table 'Practices'
ALTER TABLE [dbo].[Practices]
ADD CONSTRAINT [PK_Practices]
    PRIMARY KEY CLUSTERED ([RegNum] ASC);
GO

-- Creating primary key on [VetId] in table 'Vets'
ALTER TABLE [dbo].[Vets]
ADD CONSTRAINT [PK_Vets]
    PRIMARY KEY CLUSTERED ([VetId] ASC);
GO

-- Creating primary key on [VisitId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [PK_Visits]
    PRIMARY KEY CLUSTERED ([VisitId] ASC);
GO

-- Creating primary key on [TreatmentId] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [PK_Treatments]
    PRIMARY KEY CLUSTERED ([TreatmentId] ASC);
GO

-- Creating primary key on [MedicationId] in table 'Medications'
ALTER TABLE [dbo].[Medications]
ADD CONSTRAINT [PK_Medications]
    PRIMARY KEY CLUSTERED ([MedicationId] ASC);
GO

-- Creating primary key on [Visits_VisitId], [Medications_MedicationId] in table 'VisitMedication'
ALTER TABLE [dbo].[VisitMedication]
ADD CONSTRAINT [PK_VisitMedication]
    PRIMARY KEY CLUSTERED ([Visits_VisitId], [Medications_MedicationId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PracticeRegNum] in table 'Vets'
ALTER TABLE [dbo].[Vets]
ADD CONSTRAINT [FK_PracticeVet]
    FOREIGN KEY ([PracticeRegNum])
    REFERENCES [dbo].[Practices]
        ([RegNum])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeVet'
CREATE INDEX [IX_FK_PracticeVet]
ON [dbo].[Vets]
    ([PracticeRegNum]);
GO

-- Creating foreign key on [OwnerOwnerId] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [FK_OwnerPet]
    FOREIGN KEY ([OwnerOwnerId])
    REFERENCES [dbo].[Owners]
        ([OwnerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerPet'
CREATE INDEX [IX_FK_OwnerPet]
ON [dbo].[Pets]
    ([OwnerOwnerId]);
GO

-- Creating foreign key on [PetPetId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_PetVisit]
    FOREIGN KEY ([PetPetId])
    REFERENCES [dbo].[Pets]
        ([PetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PetVisit'
CREATE INDEX [IX_FK_PetVisit]
ON [dbo].[Visits]
    ([PetPetId]);
GO

-- Creating foreign key on [VetVetId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_VetVisit]
    FOREIGN KEY ([VetVetId])
    REFERENCES [dbo].[Vets]
        ([VetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VetVisit'
CREATE INDEX [IX_FK_VetVisit]
ON [dbo].[Visits]
    ([VetVetId]);
GO

-- Creating foreign key on [Treatment_TreatmentId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_VisitTreatment]
    FOREIGN KEY ([Treatment_TreatmentId])
    REFERENCES [dbo].[Treatments]
        ([TreatmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitTreatment'
CREATE INDEX [IX_FK_VisitTreatment]
ON [dbo].[Visits]
    ([Treatment_TreatmentId]);
GO

-- Creating foreign key on [Visits_VisitId] in table 'VisitMedication'
ALTER TABLE [dbo].[VisitMedication]
ADD CONSTRAINT [FK_VisitMedication_Visit]
    FOREIGN KEY ([Visits_VisitId])
    REFERENCES [dbo].[Visits]
        ([VisitId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Medications_MedicationId] in table 'VisitMedication'
ALTER TABLE [dbo].[VisitMedication]
ADD CONSTRAINT [FK_VisitMedication_Medication]
    FOREIGN KEY ([Medications_MedicationId])
    REFERENCES [dbo].[Medications]
        ([MedicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitMedication_Medication'
CREATE INDEX [IX_FK_VisitMedication_Medication]
ON [dbo].[VisitMedication]
    ([Medications_MedicationId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------