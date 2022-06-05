insert into "user" ("email",
                    "first_name",
                    "last_name",
                    "salt",
                    "password",
                    "created_at",
                    "updated_at",
                    "is_soft_deleted")
values (@email,
        @firstName,
        @lastName,
        @salt,
        @password,
        @createdAt,
        @updatedAt,
        @isSoftDeleted)
returning *;