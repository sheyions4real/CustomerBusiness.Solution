-- ========================================
-- 1. Tenant Table
-- ========================================
CREATE TABLE Tenants (
    TenantId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Sn INT IDENTITY(1,1),
    Name NVARCHAR(150) NOT NULL,
    Domain NVARCHAR(150),
    ContactEmail NVARCHAR(150),
    ContactPhone NVARCHAR(50),
    Status NVARCHAR(50) DEFAULT 'Active',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL
);

-- ========================================
-- 2. Business Table
-- ========================================
CREATE TABLE Businesses (
    BusinessId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(150),
    Description NVARCHAR(500),
    Email NVARCHAR(150),
    Phone NVARCHAR(50),
    Website NVARCHAR(200),
    Status NVARCHAR(50) DEFAULT 'Active',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Business_Tenant FOREIGN KEY (TenantId) REFERENCES Tenants(TenantId)
);

-- ========================================
-- 3. Address Table
-- ========================================
CREATE TABLE Addresses (
    AddressId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Street NVARCHAR(255),
    City NVARCHAR(100),
    Province NVARCHAR(100),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(100)
);

-- ========================================
-- 4. Customer Table
-- ========================================
CREATE TABLE Customers (
    CustomerId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BusinessId UNIQUEIDENTIFIER NOT NULL,
    FullName NVARCHAR(150),
    Email NVARCHAR(150),
    Phone NVARCHAR(50),
    AddressId UNIQUEIDENTIFIER NULL,
    Notes NVARCHAR(500),
    Status NVARCHAR(50) DEFAULT 'Active',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Customer_Business FOREIGN KEY (BusinessId) REFERENCES Businesses(BusinessId),
    CONSTRAINT FK_Customer_Address FOREIGN KEY (AddressId) REFERENCES Addresses(AddressId)
);

-- ========================================
-- 5. Product Table
-- ========================================
CREATE TABLE Products (
    ProductId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BusinessId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(150),
    Description NVARCHAR(500),
    Price DECIMAL(18,2),
    StockQuantity INT,
    Category NVARCHAR(100),
    SKU NVARCHAR(100),
    Status NVARCHAR(50) DEFAULT 'Active',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Product_Business FOREIGN KEY (BusinessId) REFERENCES Businesses(BusinessId)
);

-- ========================================
-- 6. Service Table
-- ========================================
CREATE TABLE Services (
    ServiceId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BusinessId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(150),
    Description NVARCHAR(500),
    Price DECIMAL(18,2),
    Duration INT,
    Status NVARCHAR(50) DEFAULT 'Active',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Service_Business FOREIGN KEY (BusinessId) REFERENCES Businesses(BusinessId)
);

-- ========================================
-- 7. Schedule Table
-- ========================================
CREATE TABLE Schedules (
    ScheduleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    ServiceId UNIQUEIDENTIFIER NOT NULL,
    BusinessId UNIQUEIDENTIFIER NOT NULL,
    ScheduledDate DATETIME2,
    StartTime TIME,
    EndTime TIME,
    Status NVARCHAR(50) DEFAULT 'Pending',
    Notes NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Schedule_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    CONSTRAINT FK_Schedule_Service FOREIGN KEY (ServiceId) REFERENCES Services(ServiceId),
    CONSTRAINT FK_Schedule_Business FOREIGN KEY (BusinessId) REFERENCES Businesses(BusinessId)
);

-- ========================================
-- 8. Sale Table
-- ========================================
CREATE TABLE Sales (
    SaleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    BusinessId UNIQUEIDENTIFIER NOT NULL,
    TotalAmount DECIMAL(18,2),
    SaleDate DATETIME2 DEFAULT SYSDATETIME(),
    Status NVARCHAR(50) DEFAULT 'Completed',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    ModifiedAt DATETIME2 NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Sale_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    CONSTRAINT FK_Sale_Business FOREIGN KEY (BusinessId) REFERENCES Businesses(BusinessId)
);

-- ========================================
-- 9. SaleItems Table
-- ========================================
CREATE TABLE SaleItems (
    SaleItemId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    SaleId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT,
    UnitPrice DECIMAL(18,2),
    Subtotal AS (Quantity * UnitPrice) PERSISTED,
    CONSTRAINT FK_SaleItem_Sale FOREIGN KEY (SaleId) REFERENCES Sales(SaleId),
    CONSTRAINT FK_SaleItem_Product FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- ========================================
-- 10. User Table
-- ========================================
CREATE TABLE Users (
    UserId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    FullName NVARCHAR(150),
    Email NVARCHAR(150) UNIQUE,
    PasswordHash NVARCHAR(255),
    Role NVARCHAR(50),
    Provider NVARCHAR(50),
    ProviderUserId NVARCHAR(150),
    IsEmailVerified BIT DEFAULT 0,
    Status NVARCHAR(50) DEFAULT 'Active',
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    ModifiedAt DATETIME2 NULL,
    CONSTRAINT FK_User_Tenant FOREIGN KEY (TenantId) REFERENCES Tenants(TenantId)
);

-- ========================================
-- 11. LoginTokens Table
-- ========================================
CREATE TABLE LoginTokens (
    LoginId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    AccessToken NVARCHAR(500),
    RefreshToken NVARCHAR(500),
    ExpiresAt DATETIME2,
    DeviceInfo NVARCHAR(150),
    IPAddress NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CONSTRAINT FK_LoginToken_User FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


-- ========================================
-- 12. Appointments Table
-- ========================================
CREATE TABLE Appointments
(
    AppointmentId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ScheduleId INT NOT NULL,                  -- FK to Schedules table
    CustomerName NVARCHAR(100) NOT NULL,     -- Who booked the appointment
    CustomerEmail NVARCHAR(100) NULL,
    AppointmentDateTime DATETIME NOT NULL,   -- Exact date/time of appointment
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',  -- e.g., Pending, Confirmed, Cancelled
    Notes NVARCHAR(MAX) NULL,                -- Optional notes
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,

    CONSTRAINT FK_Appointments_Schedules FOREIGN KEY (ScheduleId)
        REFERENCES Schedules(ScheduleId)
        ON DELETE CASCADE
);