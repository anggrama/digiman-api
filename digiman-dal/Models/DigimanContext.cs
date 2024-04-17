using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace digiman_dal.Models;

public partial class DigimanContext : DbContext
{
    public DigimanContext()
    {
    }

    public DigimanContext(DbContextOptions<DigimanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DmsDocument> DmsDocuments { get; set; }

    public virtual DbSet<DmsDocumentDetail> DmsDocumentDetails { get; set; }

    public virtual DbSet<DmsDocumentFile> DmsDocumentFiles { get; set; }

    public virtual DbSet<DmsDocumentPinned> DmsDocumentPinneds { get; set; }

    public virtual DbSet<DmsDocumentRelation> DmsDocumentRelations { get; set; }

    public virtual DbSet<DmsObject> DmsObjects { get; set; }

    public virtual DbSet<DmsObjectPermission> DmsObjectPermissions { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    public virtual DbSet<SysClaim> SysClaims { get; set; }

    public virtual DbSet<SysDocumentProfile> SysDocumentProfiles { get; set; }

    public virtual DbSet<SysDocumentProfileDetail> SysDocumentProfileDetails { get; set; }

    public virtual DbSet<SysLog> SysLogs { get; set; }

    public virtual DbSet<SysNotification> SysNotifications { get; set; }

    public virtual DbSet<SysParameter> SysParameters { get; set; }

    public virtual DbSet<SysProcessQueue> SysProcessQueues { get; set; }

    public virtual DbSet<SysStorage> SysStorages { get; set; }

    public virtual DbSet<SysStorageType> SysStorageTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=database.tokotita.com,6001;Database=digiman-v1;uid=sdkadmin;pwd=123Sdk!@#;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmsDocument>(entity =>
        {
            entity.ToTable("dms_documents", "digidocu");

            entity.HasIndex(e => e.ObjectId, "IX_dms_documents_object_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EffectiveEndDate).HasColumnName("effective_end_date");
            entity.Property(e => e.EffectiveStartDate).HasColumnName("effective_start_date");
            entity.Property(e => e.IsCheckout).HasColumnName("is_checkout");
            entity.Property(e => e.IsObsolete).HasColumnName("is_obsolete");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .HasColumnName("priority");
            entity.Property(e => e.ReferenceNumber).HasColumnName("reference_number");
            entity.Property(e => e.ReminderSettings).HasColumnName("reminder_settings");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Version)
                .HasMaxLength(10)
                .HasColumnName("version");

            entity.HasOne(d => d.Object).WithMany(p => p.DmsDocuments).HasForeignKey(d => d.ObjectId);
        });

        modelBuilder.Entity<DmsDocumentDetail>(entity =>
        {
            entity.ToTable("dms_document_details", "digidocu");

            entity.HasIndex(e => e.DocumentId, "IX_dms_document_details_document_id");

            entity.HasIndex(e => e.SysDocumentProfileDetailId, "IX_dms_document_details_sys_document_profile_detail_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DateValue)
                .HasColumnType("datetime")
                .HasColumnName("date_value");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.FieldValue).HasColumnName("field_value");
            entity.Property(e => e.NumericValue).HasColumnName("numeric_value");
            entity.Property(e => e.SysDocumentProfileDetailId).HasColumnName("sys_document_profile_detail_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Document).WithMany(p => p.DmsDocumentDetails).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.SysDocumentProfileDetail).WithMany(p => p.DmsDocumentDetails).HasForeignKey(d => d.SysDocumentProfileDetailId);
        });

        modelBuilder.Entity<DmsDocumentFile>(entity =>
        {
            entity.ToTable("dms_document_files", "digidocu");

            entity.HasIndex(e => e.DocumentId, "IX_dms_document_files_document_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Annotation).HasColumnName("annotation");
            entity.Property(e => e.ContentType).HasColumnName("content_type");
            entity.Property(e => e.ConvertedFilename)
                .HasMaxLength(50)
                .HasColumnName("converted_filename");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.IsEncrypt).HasColumnName("is_encrypt");
            entity.Property(e => e.IsOcr).HasColumnName("is_ocr");
            entity.Property(e => e.OriginalFilename)
                .HasMaxLength(255)
                .HasColumnName("original_filename");
            entity.Property(e => e.Size).HasColumnName("size");

            entity.HasOne(d => d.Document).WithMany(p => p.DmsDocumentFiles)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DmsDocumentPinned>(entity =>
        {
            entity.ToTable("dms_document_pinned", "digidocu");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<DmsDocumentRelation>(entity =>
        {
            entity.ToTable("dms_document_relations", "digidocu");

            entity.HasIndex(e => e.DocumentId, "IX_dms_document_relations_document_id");

            entity.HasIndex(e => e.DocumentRefId, "IX_dms_document_relations_document_ref_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.DocumentRefId).HasColumnName("document_ref_id");

            entity.HasOne(d => d.Document).WithMany(p => p.DmsDocumentRelationDocuments).HasForeignKey(d => d.DocumentId);

            entity.HasOne(d => d.DocumentRef).WithMany(p => p.DmsDocumentRelationDocumentRefs).HasForeignKey(d => d.DocumentRefId);
        });

        modelBuilder.Entity<DmsObject>(entity =>
        {
            entity.ToTable("dms_objects", "digidocu");

            entity.HasIndex(e => e.CreatedBy, "IX_dms_objects_created_by");

            entity.HasIndex(e => e.DefaultStorageId, "IX_dms_objects_default_storage_id");

            entity.HasIndex(e => e.DocumentProfileId, "IX_dms_objects_document_profile_id");

            entity.HasIndex(e => e.RootId, "IX_dms_objects_root_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DefaultStorageId).HasColumnName("default_storage_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DocumentProfileDefault)
                .IsUnicode(false)
                .HasColumnName("document_profile_default");
            entity.Property(e => e.DocumentProfileId).HasColumnName("document_profile_id");
            entity.Property(e => e.HierarchyLevel).HasColumnName("hierarchy_level");
            entity.Property(e => e.IsInheritPermission).HasColumnName("is_inherit_permission");
            entity.Property(e => e.LimitStorage).HasColumnName("limit_storage");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ObjectPermission).HasColumnName("object_permission");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.RecordStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("record_status");
            entity.Property(e => e.RootId).HasColumnName("root_id");
            entity.Property(e => e.ScopeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("scope_type");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DmsObjects).HasForeignKey(d => d.CreatedBy);

            entity.HasOne(d => d.DefaultStorage).WithMany(p => p.DmsObjects).HasForeignKey(d => d.DefaultStorageId);

            entity.HasOne(d => d.DocumentProfile).WithMany(p => p.DmsObjects).HasForeignKey(d => d.DocumentProfileId);

            entity.HasOne(d => d.Root).WithMany(p => p.InverseRoot).HasForeignKey(d => d.RootId);
        });

        modelBuilder.Entity<DmsObjectPermission>(entity =>
        {
            entity.ToTable("dms_object_permissions", "digidocu");

            entity.HasIndex(e => e.ObjectId, "IX_dms_object_permissions_object_id");

            entity.HasIndex(e => e.UserId, "IX_dms_object_permissions_user_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.PermissionSettings).HasColumnName("permission_settings");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserType).HasColumnName("user_type");

            entity.HasOne(d => d.Object).WithMany(p => p.DmsObjectPermissions).HasForeignKey(d => d.ObjectId);

            entity.HasOne(d => d.User).WithMany(p => p.DmsObjectPermissions).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("groups");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasMany(d => d.Roles).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupRole",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                    j =>
                    {
                        j.HasKey("GroupId", "RoleId");
                        j.ToTable("group_roles");
                        j.HasIndex(new[] { "GroupId" }, "IX_group_roles_group_id");
                        j.HasIndex(new[] { "RoleId" }, "IX_group_roles_role_id");
                        j.IndexerProperty<Guid>("GroupId").HasColumnName("group_id");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Message).HasColumnName("message");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NormalizedName).HasColumnName("normalized_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j =>
                    {
                        j.HasKey("RoleId", "UserId");
                        j.ToTable("user_roles");
                        j.HasIndex(new[] { "UserId" }, "IX_user_roles_user_id");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("role_claims");

            entity.HasIndex(e => e.RoleId, "IX_role_claims_role_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClaimId).HasColumnName("claim_id");
            entity.Property(e => e.ClaimValue).HasColumnName("claim_value");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<SysClaim>(entity =>
        {
            entity.ToTable("sys_claims");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClaimsTemplateValue).HasColumnName("claims_template_value");
            entity.Property(e => e.ClaimsType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("M=Module C = Category V = View")
                .HasColumnName("claims_type");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(200)
                .HasColumnName("display_name");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
        });

        modelBuilder.Entity<SysDocumentProfile>(entity =>
        {
            entity.ToTable("sys_document_profiles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AutonumberFormat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autonumber_format");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.IsSetDocumentProfile).HasColumnName("is_set_document_profile");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentProfileId).HasColumnName("parent_profile_id");
            entity.Property(e => e.RecordStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("record_status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WatermarkFile).HasColumnName("watermark_file");
        });

        modelBuilder.Entity<SysDocumentProfileDetail>(entity =>
        {
            entity.ToTable("sys_document_profile_detail");

            entity.HasIndex(e => e.DocumentProfileId, "IX_sys_document_profile_detail_document_profile_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DisplaySeq).HasColumnName("display_seq");
            entity.Property(e => e.DocumentProfileId).HasColumnName("document_profile_id");
            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .HasColumnName("field_name");
            entity.Property(e => e.FieldOptions).HasColumnName("field_options");
            entity.Property(e => e.FieldType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("field_type");
            entity.Property(e => e.Mandatory).HasColumnName("mandatory");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.DocumentProfile).WithMany(p => p.SysDocumentProfileDetails).HasForeignKey(d => d.DocumentProfileId);
        });

        modelBuilder.Entity<SysLog>(entity =>
        {
            entity.ToTable("SysLog");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SysNotification>(entity =>
        {
            entity.ToTable("sys_notifications");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AvailableParameter).HasColumnName("available_parameter");
            entity.Property(e => e.DefaultMessage).HasColumnName("default_message");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("notification_type");
        });

        modelBuilder.Entity<SysParameter>(entity =>
        {
            entity.ToTable("sys_parameters");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DefaultValue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("default_value");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("value");
            entity.Property(e => e.VisibleStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("visible_status");
        });

        modelBuilder.Entity<SysProcessQueue>(entity =>
        {
            entity.ToTable("SysProcessQueue");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SysStorage>(entity =>
        {
            entity.ToTable("sys_storages");

            entity.HasIndex(e => e.StorageTypeId, "IX_sys_storages_storage_type_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.StorageOptions).HasColumnName("storage_options");
            entity.Property(e => e.StorageTypeId).HasColumnName("storage_type_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.StorageType).WithMany(p => p.SysStorages)
                .HasForeignKey(d => d.StorageTypeId)
                .HasConstraintName("FK_sys_storages_sys_storage_types");
        });

        modelBuilder.Entity<SysStorageType>(entity =>
        {
            entity.ToTable("sys_storage_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("display_name");
            entity.Property(e => e.IconClass).HasColumnName("icon_class");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OptionsTemplate).HasColumnName("options_template");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccessFailedCount).HasColumnName("access_failed_count");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(200)
                .HasColumnName("fullname");
            entity.Property(e => e.IsLdap).HasColumnName("is_ldap");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.LockoutEnabled).HasColumnName("lockout_enabled");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.ProfileSettings)
                .IsUnicode(false)
                .HasColumnName("profile_settings");
            entity.Property(e => e.RecordStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("record_status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");

            entity.HasMany(d => d.Groups).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    r => r.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "GroupId");
                        j.ToTable("user_groups");
                        j.HasIndex(new[] { "GroupId" }, "IX_user_groups_group_id");
                        j.HasIndex(new[] { "UserId" }, "IX_user_groups_user_id");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<Guid>("GroupId").HasColumnName("group_id");
                    });
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.ToTable("user_claims");

            entity.HasIndex(e => e.UserId, "IX_user_claims_user_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClaimId).HasColumnName("claim_id");
            entity.Property(e => e.ClaimValue).HasColumnName("claim_value");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.ToTable("user_login");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.LoginProvider).HasColumnName("login_provider");
            entity.Property(e => e.ProviderDisplayName).HasColumnName("provider_display_name");
            entity.Property(e => e.ProviderKey).HasColumnName("provider_key");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.ToTable("user_notifications");

            entity.HasIndex(e => e.NotificationId, "IX_user_notifications_notification_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsNew).HasColumnName("is_new");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.ReadDate)
                .HasColumnType("datetime")
                .HasColumnName("read_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Notification).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_user_notifications_notifications");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable("user_token");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.LoginProvider).HasColumnName("login_provider");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
