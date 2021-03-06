PGDMP                         x            keycloak    13.0 (Debian 13.0-1.pgdg100+1)    13.0 �   C           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            D           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            E           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            F           1262    16384    keycloak    DATABASE     \   CREATE DATABASE keycloak WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';
    DROP DATABASE keycloak;
                keycloak    false            �            1259    17044    admin_event_entity    TABLE     �  CREATE TABLE public.admin_event_entity (
    id character varying(36) NOT NULL,
    admin_event_time bigint,
    realm_id character varying(255),
    operation_type character varying(255),
    auth_realm_id character varying(255),
    auth_client_id character varying(255),
    auth_user_id character varying(255),
    ip_address character varying(255),
    resource_path character varying(2550),
    representation text,
    error character varying(255),
    resource_type character varying(64)
);
 &   DROP TABLE public.admin_event_entity;
       public         heap    keycloak    false                       1259    17507    associated_policy    TABLE     �   CREATE TABLE public.associated_policy (
    policy_id character varying(36) NOT NULL,
    associated_policy_id character varying(36) NOT NULL
);
 %   DROP TABLE public.associated_policy;
       public         heap    keycloak    false            �            1259    17062    authentication_execution    TABLE     �  CREATE TABLE public.authentication_execution (
    id character varying(36) NOT NULL,
    alias character varying(255),
    authenticator character varying(36),
    realm_id character varying(36),
    flow_id character varying(36),
    requirement integer,
    priority integer,
    authenticator_flow boolean DEFAULT false NOT NULL,
    auth_flow_id character varying(36),
    auth_config character varying(36)
);
 ,   DROP TABLE public.authentication_execution;
       public         heap    keycloak    false            �            1259    17056    authentication_flow    TABLE     t  CREATE TABLE public.authentication_flow (
    id character varying(36) NOT NULL,
    alias character varying(255),
    description character varying(255),
    realm_id character varying(36),
    provider_id character varying(36) DEFAULT 'basic-flow'::character varying NOT NULL,
    top_level boolean DEFAULT false NOT NULL,
    built_in boolean DEFAULT false NOT NULL
);
 '   DROP TABLE public.authentication_flow;
       public         heap    keycloak    false            �            1259    17050    authenticator_config    TABLE     �   CREATE TABLE public.authenticator_config (
    id character varying(36) NOT NULL,
    alias character varying(255),
    realm_id character varying(36)
);
 (   DROP TABLE public.authenticator_config;
       public         heap    keycloak    false            �            1259    17067    authenticator_config_entry    TABLE     �   CREATE TABLE public.authenticator_config_entry (
    authenticator_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);
 .   DROP TABLE public.authenticator_config_entry;
       public         heap    keycloak    false                       1259    17522    broker_link    TABLE     L  CREATE TABLE public.broker_link (
    identity_provider character varying(255) NOT NULL,
    storage_provider_id character varying(255),
    realm_id character varying(36) NOT NULL,
    broker_user_id character varying(255),
    broker_username character varying(255),
    token text,
    user_id character varying(255) NOT NULL
);
    DROP TABLE public.broker_link;
       public         heap    keycloak    false            �            1259    16399    client    TABLE     �  CREATE TABLE public.client (
    id character varying(36) NOT NULL,
    enabled boolean DEFAULT false NOT NULL,
    full_scope_allowed boolean DEFAULT false NOT NULL,
    client_id character varying(255),
    not_before integer,
    public_client boolean DEFAULT false NOT NULL,
    secret character varying(255),
    base_url character varying(255),
    bearer_only boolean DEFAULT false NOT NULL,
    management_url character varying(255),
    surrogate_auth_required boolean DEFAULT false NOT NULL,
    realm_id character varying(36),
    protocol character varying(255),
    node_rereg_timeout integer DEFAULT 0,
    frontchannel_logout boolean DEFAULT false NOT NULL,
    consent_required boolean DEFAULT false NOT NULL,
    name character varying(255),
    service_accounts_enabled boolean DEFAULT false NOT NULL,
    client_authenticator_type character varying(255),
    root_url character varying(255),
    description character varying(255),
    registration_token character varying(255),
    standard_flow_enabled boolean DEFAULT true NOT NULL,
    implicit_flow_enabled boolean DEFAULT false NOT NULL,
    direct_access_grants_enabled boolean DEFAULT false NOT NULL,
    always_display_in_console boolean DEFAULT false NOT NULL
);
    DROP TABLE public.client;
       public         heap    keycloak    false            �            1259    16773    client_attributes    TABLE     �   CREATE TABLE public.client_attributes (
    client_id character varying(36) NOT NULL,
    value character varying(4000),
    name character varying(255) NOT NULL
);
 %   DROP TABLE public.client_attributes;
       public         heap    keycloak    false                       1259    17781    client_auth_flow_bindings    TABLE     �   CREATE TABLE public.client_auth_flow_bindings (
    client_id character varying(36) NOT NULL,
    flow_id character varying(36),
    binding_name character varying(255) NOT NULL
);
 -   DROP TABLE public.client_auth_flow_bindings;
       public         heap    keycloak    false            �            1259    16396    client_default_roles    TABLE     �   CREATE TABLE public.client_default_roles (
    client_id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL
);
 (   DROP TABLE public.client_default_roles;
       public         heap    keycloak    false                       1259    17656    client_initial_access    TABLE     �   CREATE TABLE public.client_initial_access (
    id character varying(36) NOT NULL,
    realm_id character varying(36) NOT NULL,
    "timestamp" integer,
    expiration integer,
    count integer,
    remaining_count integer
);
 )   DROP TABLE public.client_initial_access;
       public         heap    keycloak    false            �            1259    16785    client_node_registrations    TABLE     �   CREATE TABLE public.client_node_registrations (
    client_id character varying(36) NOT NULL,
    value integer,
    name character varying(255) NOT NULL
);
 -   DROP TABLE public.client_node_registrations;
       public         heap    keycloak    false                       1259    17305    client_scope    TABLE     �   CREATE TABLE public.client_scope (
    id character varying(36) NOT NULL,
    name character varying(255),
    realm_id character varying(36),
    description character varying(255),
    protocol character varying(255)
);
     DROP TABLE public.client_scope;
       public         heap    keycloak    false                       1259    17320    client_scope_attributes    TABLE     �   CREATE TABLE public.client_scope_attributes (
    scope_id character varying(36) NOT NULL,
    value character varying(2048),
    name character varying(255) NOT NULL
);
 +   DROP TABLE public.client_scope_attributes;
       public         heap    keycloak    false                       1259    17823    client_scope_client    TABLE     �   CREATE TABLE public.client_scope_client (
    client_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL,
    default_scope boolean DEFAULT false NOT NULL
);
 '   DROP TABLE public.client_scope_client;
       public         heap    keycloak    false                       1259    17326    client_scope_role_mapping    TABLE     �   CREATE TABLE public.client_scope_role_mapping (
    scope_id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL
);
 -   DROP TABLE public.client_scope_role_mapping;
       public         heap    keycloak    false            �            1259    16411    client_session    TABLE     �  CREATE TABLE public.client_session (
    id character varying(36) NOT NULL,
    client_id character varying(36),
    redirect_uri character varying(255),
    state character varying(255),
    "timestamp" integer,
    session_id character varying(36),
    auth_method character varying(255),
    realm_id character varying(255),
    auth_user_id character varying(36),
    current_action character varying(36)
);
 "   DROP TABLE public.client_session;
       public         heap    keycloak    false            �            1259    17088    client_session_auth_status    TABLE     �   CREATE TABLE public.client_session_auth_status (
    authenticator character varying(36) NOT NULL,
    status integer,
    client_session character varying(36) NOT NULL
);
 .   DROP TABLE public.client_session_auth_status;
       public         heap    keycloak    false            �            1259    16779    client_session_note    TABLE     �   CREATE TABLE public.client_session_note (
    name character varying(255) NOT NULL,
    value character varying(255),
    client_session character varying(36) NOT NULL
);
 '   DROP TABLE public.client_session_note;
       public         heap    keycloak    false            �            1259    16966    client_session_prot_mapper    TABLE     �   CREATE TABLE public.client_session_prot_mapper (
    protocol_mapper_id character varying(36) NOT NULL,
    client_session character varying(36) NOT NULL
);
 .   DROP TABLE public.client_session_prot_mapper;
       public         heap    keycloak    false            �            1259    16417    client_session_role    TABLE     �   CREATE TABLE public.client_session_role (
    role_id character varying(255) NOT NULL,
    client_session character varying(36) NOT NULL
);
 '   DROP TABLE public.client_session_role;
       public         heap    keycloak    false            �            1259    17169    client_user_session_note    TABLE     �   CREATE TABLE public.client_user_session_note (
    name character varying(255) NOT NULL,
    value character varying(2048),
    client_session character varying(36) NOT NULL
);
 ,   DROP TABLE public.client_user_session_note;
       public         heap    keycloak    false                       1259    17572 	   component    TABLE     )  CREATE TABLE public.component (
    id character varying(36) NOT NULL,
    name character varying(255),
    parent_id character varying(36),
    provider_id character varying(36),
    provider_type character varying(255),
    realm_id character varying(36),
    sub_type character varying(255)
);
    DROP TABLE public.component;
       public         heap    keycloak    false                       1259    17566    component_config    TABLE     �   CREATE TABLE public.component_config (
    id character varying(36) NOT NULL,
    component_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(4000)
);
 $   DROP TABLE public.component_config;
       public         heap    keycloak    false            �            1259    16420    composite_role    TABLE     �   CREATE TABLE public.composite_role (
    composite character varying(36) NOT NULL,
    child_role character varying(36) NOT NULL
);
 "   DROP TABLE public.composite_role;
       public         heap    keycloak    false            �            1259    16423 
   credential    TABLE     $  CREATE TABLE public.credential (
    id character varying(36) NOT NULL,
    salt bytea,
    type character varying(255),
    user_id character varying(36),
    created_date bigint,
    user_label character varying(255),
    secret_data text,
    credential_data text,
    priority integer
);
    DROP TABLE public.credential;
       public         heap    keycloak    false            �            1259    16390    databasechangelog    TABLE     Y  CREATE TABLE public.databasechangelog (
    id character varying(255) NOT NULL,
    author character varying(255) NOT NULL,
    filename character varying(255) NOT NULL,
    dateexecuted timestamp without time zone NOT NULL,
    orderexecuted integer NOT NULL,
    exectype character varying(10) NOT NULL,
    md5sum character varying(35),
    description character varying(255),
    comments character varying(255),
    tag character varying(255),
    liquibase character varying(20),
    contexts character varying(255),
    labels character varying(255),
    deployment_id character varying(10)
);
 %   DROP TABLE public.databasechangelog;
       public         heap    keycloak    false            �            1259    16385    databasechangeloglock    TABLE     �   CREATE TABLE public.databasechangeloglock (
    id integer NOT NULL,
    locked boolean NOT NULL,
    lockgranted timestamp without time zone,
    lockedby character varying(255)
);
 )   DROP TABLE public.databasechangeloglock;
       public         heap    keycloak    false                       1259    17839    default_client_scope    TABLE     �   CREATE TABLE public.default_client_scope (
    realm_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL,
    default_scope boolean DEFAULT false NOT NULL
);
 (   DROP TABLE public.default_client_scope;
       public         heap    keycloak    false            �            1259    16429    event_entity    TABLE     �  CREATE TABLE public.event_entity (
    id character varying(36) NOT NULL,
    client_id character varying(255),
    details_json character varying(2550),
    error character varying(255),
    ip_address character varying(255),
    realm_id character varying(255),
    session_id character varying(255),
    event_time bigint,
    type character varying(255),
    user_id character varying(255)
);
     DROP TABLE public.event_entity;
       public         heap    keycloak    false                       1259    17528    fed_user_attribute    TABLE     (  CREATE TABLE public.fed_user_attribute (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36),
    value character varying(2024)
);
 &   DROP TABLE public.fed_user_attribute;
       public         heap    keycloak    false                       1259    17534    fed_user_consent    TABLE     �  CREATE TABLE public.fed_user_consent (
    id character varying(36) NOT NULL,
    client_id character varying(255),
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36),
    created_date bigint,
    last_updated_date bigint,
    client_storage_provider character varying(36),
    external_client_id character varying(255)
);
 $   DROP TABLE public.fed_user_consent;
       public         heap    keycloak    false                        1259    17865    fed_user_consent_cl_scope    TABLE     �   CREATE TABLE public.fed_user_consent_cl_scope (
    user_consent_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL
);
 -   DROP TABLE public.fed_user_consent_cl_scope;
       public         heap    keycloak    false                       1259    17543    fed_user_credential    TABLE     �  CREATE TABLE public.fed_user_credential (
    id character varying(36) NOT NULL,
    salt bytea,
    type character varying(255),
    created_date bigint,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36),
    user_label character varying(255),
    secret_data text,
    credential_data text,
    priority integer
);
 '   DROP TABLE public.fed_user_credential;
       public         heap    keycloak    false                       1259    17553    fed_user_group_membership    TABLE     �   CREATE TABLE public.fed_user_group_membership (
    group_id character varying(36) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36)
);
 -   DROP TABLE public.fed_user_group_membership;
       public         heap    keycloak    false                       1259    17556    fed_user_required_action    TABLE       CREATE TABLE public.fed_user_required_action (
    required_action character varying(255) DEFAULT ' '::character varying NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36)
);
 ,   DROP TABLE public.fed_user_required_action;
       public         heap    keycloak    false                       1259    17563    fed_user_role_mapping    TABLE     �   CREATE TABLE public.fed_user_role_mapping (
    role_id character varying(36) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36)
);
 )   DROP TABLE public.fed_user_role_mapping;
       public         heap    keycloak    false            �            1259    16823    federated_identity    TABLE       CREATE TABLE public.federated_identity (
    identity_provider character varying(255) NOT NULL,
    realm_id character varying(36),
    federated_user_id character varying(255),
    federated_username character varying(255),
    token text,
    user_id character varying(36) NOT NULL
);
 &   DROP TABLE public.federated_identity;
       public         heap    keycloak    false                       1259    17632    federated_user    TABLE     �   CREATE TABLE public.federated_user (
    id character varying(255) NOT NULL,
    storage_provider_id character varying(255),
    realm_id character varying(36) NOT NULL
);
 "   DROP TABLE public.federated_user;
       public         heap    keycloak    false                       1259    17242    group_attribute    TABLE       CREATE TABLE public.group_attribute (
    id character varying(36) DEFAULT 'sybase-needs-something-here'::character varying NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(255),
    group_id character varying(36) NOT NULL
);
 #   DROP TABLE public.group_attribute;
       public         heap    keycloak    false                       1259    17239    group_role_mapping    TABLE     �   CREATE TABLE public.group_role_mapping (
    role_id character varying(36) NOT NULL,
    group_id character varying(36) NOT NULL
);
 &   DROP TABLE public.group_role_mapping;
       public         heap    keycloak    false            �            1259    16829    identity_provider    TABLE     �  CREATE TABLE public.identity_provider (
    internal_id character varying(36) NOT NULL,
    enabled boolean DEFAULT false NOT NULL,
    provider_alias character varying(255),
    provider_id character varying(255),
    store_token boolean DEFAULT false NOT NULL,
    authenticate_by_default boolean DEFAULT false NOT NULL,
    realm_id character varying(36),
    add_token_role boolean DEFAULT true NOT NULL,
    trust_email boolean DEFAULT false NOT NULL,
    first_broker_login_flow_id character varying(36),
    post_broker_login_flow_id character varying(36),
    provider_display_name character varying(255),
    link_only boolean DEFAULT false NOT NULL
);
 %   DROP TABLE public.identity_provider;
       public         heap    keycloak    false            �            1259    16839    identity_provider_config    TABLE     �   CREATE TABLE public.identity_provider_config (
    identity_provider_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);
 ,   DROP TABLE public.identity_provider_config;
       public         heap    keycloak    false            �            1259    16945    identity_provider_mapper    TABLE       CREATE TABLE public.identity_provider_mapper (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    idp_alias character varying(255) NOT NULL,
    idp_mapper_name character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL
);
 ,   DROP TABLE public.identity_provider_mapper;
       public         heap    keycloak    false            �            1259    16951    idp_mapper_config    TABLE     �   CREATE TABLE public.idp_mapper_config (
    idp_mapper_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);
 %   DROP TABLE public.idp_mapper_config;
       public         heap    keycloak    false                        1259    17236    keycloak_group    TABLE     �   CREATE TABLE public.keycloak_group (
    id character varying(36) NOT NULL,
    name character varying(255),
    parent_group character varying(36) NOT NULL,
    realm_id character varying(36)
);
 "   DROP TABLE public.keycloak_group;
       public         heap    keycloak    false            �            1259    16438    keycloak_role    TABLE     b  CREATE TABLE public.keycloak_role (
    id character varying(36) NOT NULL,
    client_realm_constraint character varying(255),
    client_role boolean DEFAULT false NOT NULL,
    description character varying(255),
    name character varying(255),
    realm_id character varying(255),
    client character varying(36),
    realm character varying(36)
);
 !   DROP TABLE public.keycloak_role;
       public         heap    keycloak    false            �            1259    16942    migration_model    TABLE     �   CREATE TABLE public.migration_model (
    id character varying(36) NOT NULL,
    version character varying(36),
    update_time bigint DEFAULT 0 NOT NULL
);
 #   DROP TABLE public.migration_model;
       public         heap    keycloak    false            �            1259    17226    offline_client_session    TABLE     �  CREATE TABLE public.offline_client_session (
    user_session_id character varying(36) NOT NULL,
    client_id character varying(255) NOT NULL,
    offline_flag character varying(4) NOT NULL,
    "timestamp" integer,
    data text,
    client_storage_provider character varying(36) DEFAULT 'local'::character varying NOT NULL,
    external_client_id character varying(255) DEFAULT 'local'::character varying NOT NULL
);
 *   DROP TABLE public.offline_client_session;
       public         heap    keycloak    false            �            1259    17220    offline_user_session    TABLE     P  CREATE TABLE public.offline_user_session (
    user_session_id character varying(36) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    created_on integer NOT NULL,
    offline_flag character varying(4) NOT NULL,
    data text,
    last_session_refresh integer DEFAULT 0 NOT NULL
);
 (   DROP TABLE public.offline_user_session;
       public         heap    keycloak    false                       1259    17449    policy_config    TABLE     �   CREATE TABLE public.policy_config (
    policy_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value text
);
 !   DROP TABLE public.policy_config;
       public         heap    keycloak    false            �            1259    16810    protocol_mapper    TABLE     1  CREATE TABLE public.protocol_mapper (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    protocol character varying(255) NOT NULL,
    protocol_mapper_name character varying(255) NOT NULL,
    client_id character varying(36),
    client_scope_id character varying(36)
);
 #   DROP TABLE public.protocol_mapper;
       public         heap    keycloak    false            �            1259    16817    protocol_mapper_config    TABLE     �   CREATE TABLE public.protocol_mapper_config (
    protocol_mapper_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);
 *   DROP TABLE public.protocol_mapper_config;
       public         heap    keycloak    false            �            1259    16445    realm    TABLE     �	  CREATE TABLE public.realm (
    id character varying(36) NOT NULL,
    access_code_lifespan integer,
    user_action_lifespan integer,
    access_token_lifespan integer,
    account_theme character varying(255),
    admin_theme character varying(255),
    email_theme character varying(255),
    enabled boolean DEFAULT false NOT NULL,
    events_enabled boolean DEFAULT false NOT NULL,
    events_expiration bigint,
    login_theme character varying(255),
    name character varying(255),
    not_before integer,
    password_policy character varying(2550),
    registration_allowed boolean DEFAULT false NOT NULL,
    remember_me boolean DEFAULT false NOT NULL,
    reset_password_allowed boolean DEFAULT false NOT NULL,
    social boolean DEFAULT false NOT NULL,
    ssl_required character varying(255),
    sso_idle_timeout integer,
    sso_max_lifespan integer,
    update_profile_on_soc_login boolean DEFAULT false NOT NULL,
    verify_email boolean DEFAULT false NOT NULL,
    master_admin_client character varying(36),
    login_lifespan integer,
    internationalization_enabled boolean DEFAULT false NOT NULL,
    default_locale character varying(255),
    reg_email_as_username boolean DEFAULT false NOT NULL,
    admin_events_enabled boolean DEFAULT false NOT NULL,
    admin_events_details_enabled boolean DEFAULT false NOT NULL,
    edit_username_allowed boolean DEFAULT false NOT NULL,
    otp_policy_counter integer DEFAULT 0,
    otp_policy_window integer DEFAULT 1,
    otp_policy_period integer DEFAULT 30,
    otp_policy_digits integer DEFAULT 6,
    otp_policy_alg character varying(36) DEFAULT 'HmacSHA1'::character varying,
    otp_policy_type character varying(36) DEFAULT 'totp'::character varying,
    browser_flow character varying(36),
    registration_flow character varying(36),
    direct_grant_flow character varying(36),
    reset_credentials_flow character varying(36),
    client_auth_flow character varying(36),
    offline_session_idle_timeout integer DEFAULT 0,
    revoke_refresh_token boolean DEFAULT false NOT NULL,
    access_token_life_implicit integer DEFAULT 0,
    login_with_email_allowed boolean DEFAULT true NOT NULL,
    duplicate_emails_allowed boolean DEFAULT false NOT NULL,
    docker_auth_flow character varying(36),
    refresh_token_max_reuse integer DEFAULT 0,
    allow_user_managed_access boolean DEFAULT false NOT NULL,
    sso_max_lifespan_remember_me integer DEFAULT 0 NOT NULL,
    sso_idle_timeout_remember_me integer DEFAULT 0 NOT NULL
);
    DROP TABLE public.realm;
       public         heap    keycloak    false            �            1259    16463    realm_attribute    TABLE     �   CREATE TABLE public.realm_attribute (
    name character varying(255) NOT NULL,
    value character varying(255),
    realm_id character varying(36) NOT NULL
);
 #   DROP TABLE public.realm_attribute;
       public         heap    keycloak    false                       1259    17252    realm_default_groups    TABLE     �   CREATE TABLE public.realm_default_groups (
    realm_id character varying(36) NOT NULL,
    group_id character varying(36) NOT NULL
);
 (   DROP TABLE public.realm_default_groups;
       public         heap    keycloak    false            �            1259    16469    realm_default_roles    TABLE     �   CREATE TABLE public.realm_default_roles (
    realm_id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL
);
 '   DROP TABLE public.realm_default_roles;
       public         heap    keycloak    false            �            1259    16934    realm_enabled_event_types    TABLE     �   CREATE TABLE public.realm_enabled_event_types (
    realm_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);
 -   DROP TABLE public.realm_enabled_event_types;
       public         heap    keycloak    false            �            1259    16472    realm_events_listeners    TABLE     �   CREATE TABLE public.realm_events_listeners (
    realm_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);
 *   DROP TABLE public.realm_events_listeners;
       public         heap    keycloak    false            �            1259    16475    realm_required_credential    TABLE       CREATE TABLE public.realm_required_credential (
    type character varying(255) NOT NULL,
    form_label character varying(255),
    input boolean DEFAULT false NOT NULL,
    secret boolean DEFAULT false NOT NULL,
    realm_id character varying(36) NOT NULL
);
 -   DROP TABLE public.realm_required_credential;
       public         heap    keycloak    false            �            1259    16483    realm_smtp_config    TABLE     �   CREATE TABLE public.realm_smtp_config (
    realm_id character varying(36) NOT NULL,
    value character varying(255),
    name character varying(255) NOT NULL
);
 %   DROP TABLE public.realm_smtp_config;
       public         heap    keycloak    false            �            1259    16849    realm_supported_locales    TABLE     �   CREATE TABLE public.realm_supported_locales (
    realm_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);
 +   DROP TABLE public.realm_supported_locales;
       public         heap    keycloak    false            �            1259    16495    redirect_uris    TABLE        CREATE TABLE public.redirect_uris (
    client_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);
 !   DROP TABLE public.redirect_uris;
       public         heap    keycloak    false            �            1259    17183    required_action_config    TABLE     �   CREATE TABLE public.required_action_config (
    required_action_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);
 *   DROP TABLE public.required_action_config;
       public         heap    keycloak    false            �            1259    17175    required_action_provider    TABLE     \  CREATE TABLE public.required_action_provider (
    id character varying(36) NOT NULL,
    alias character varying(255),
    name character varying(255),
    realm_id character varying(36),
    enabled boolean DEFAULT false NOT NULL,
    default_action boolean DEFAULT false NOT NULL,
    provider_id character varying(255),
    priority integer
);
 ,   DROP TABLE public.required_action_provider;
       public         heap    keycloak    false            "           1259    17904    resource_attribute    TABLE       CREATE TABLE public.resource_attribute (
    id character varying(36) DEFAULT 'sybase-needs-something-here'::character varying NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(255),
    resource_id character varying(36) NOT NULL
);
 &   DROP TABLE public.resource_attribute;
       public         heap    keycloak    false                       1259    17477    resource_policy    TABLE     �   CREATE TABLE public.resource_policy (
    resource_id character varying(36) NOT NULL,
    policy_id character varying(36) NOT NULL
);
 #   DROP TABLE public.resource_policy;
       public         heap    keycloak    false                       1259    17462    resource_scope    TABLE     �   CREATE TABLE public.resource_scope (
    resource_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL
);
 "   DROP TABLE public.resource_scope;
       public         heap    keycloak    false                       1259    17396    resource_server    TABLE     �   CREATE TABLE public.resource_server (
    id character varying(36) NOT NULL,
    allow_rs_remote_mgmt boolean DEFAULT false NOT NULL,
    policy_enforce_mode character varying(15) NOT NULL,
    decision_strategy smallint DEFAULT 1 NOT NULL
);
 #   DROP TABLE public.resource_server;
       public         heap    keycloak    false            !           1259    17880    resource_server_perm_ticket    TABLE     �  CREATE TABLE public.resource_server_perm_ticket (
    id character varying(36) NOT NULL,
    owner character varying(255) NOT NULL,
    requester character varying(255) NOT NULL,
    created_timestamp bigint NOT NULL,
    granted_timestamp bigint,
    resource_id character varying(36) NOT NULL,
    scope_id character varying(36),
    resource_server_id character varying(36) NOT NULL,
    policy_id character varying(36)
);
 /   DROP TABLE public.resource_server_perm_ticket;
       public         heap    keycloak    false                       1259    17434    resource_server_policy    TABLE     y  CREATE TABLE public.resource_server_policy (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255),
    type character varying(255) NOT NULL,
    decision_strategy character varying(20),
    logic character varying(20),
    resource_server_id character varying(36) NOT NULL,
    owner character varying(255)
);
 *   DROP TABLE public.resource_server_policy;
       public         heap    keycloak    false            	           1259    17404    resource_server_resource    TABLE     �  CREATE TABLE public.resource_server_resource (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    type character varying(255),
    icon_uri character varying(255),
    owner character varying(255) NOT NULL,
    resource_server_id character varying(36) NOT NULL,
    owner_managed_access boolean DEFAULT false NOT NULL,
    display_name character varying(255)
);
 ,   DROP TABLE public.resource_server_resource;
       public         heap    keycloak    false            
           1259    17419    resource_server_scope    TABLE       CREATE TABLE public.resource_server_scope (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    icon_uri character varying(255),
    resource_server_id character varying(36) NOT NULL,
    display_name character varying(255)
);
 )   DROP TABLE public.resource_server_scope;
       public         heap    keycloak    false            #           1259    17923    resource_uris    TABLE     �   CREATE TABLE public.resource_uris (
    resource_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);
 !   DROP TABLE public.resource_uris;
       public         heap    keycloak    false            $           1259    17933    role_attribute    TABLE     �   CREATE TABLE public.role_attribute (
    id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(255)
);
 "   DROP TABLE public.role_attribute;
       public         heap    keycloak    false            �            1259    16498    scope_mapping    TABLE     �   CREATE TABLE public.scope_mapping (
    client_id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL
);
 !   DROP TABLE public.scope_mapping;
       public         heap    keycloak    false                       1259    17492    scope_policy    TABLE     �   CREATE TABLE public.scope_policy (
    scope_id character varying(36) NOT NULL,
    policy_id character varying(36) NOT NULL
);
     DROP TABLE public.scope_policy;
       public         heap    keycloak    false            �            1259    16504    user_attribute    TABLE     �   CREATE TABLE public.user_attribute (
    name character varying(255) NOT NULL,
    value character varying(255),
    user_id character varying(36) NOT NULL,
    id character varying(36) DEFAULT 'sybase-needs-something-here'::character varying NOT NULL
);
 "   DROP TABLE public.user_attribute;
       public         heap    keycloak    false            �            1259    16957    user_consent    TABLE     7  CREATE TABLE public.user_consent (
    id character varying(36) NOT NULL,
    client_id character varying(255),
    user_id character varying(36) NOT NULL,
    created_date bigint,
    last_updated_date bigint,
    client_storage_provider character varying(36),
    external_client_id character varying(255)
);
     DROP TABLE public.user_consent;
       public         heap    keycloak    false                       1259    17855    user_consent_client_scope    TABLE     �   CREATE TABLE public.user_consent_client_scope (
    user_consent_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL
);
 -   DROP TABLE public.user_consent_client_scope;
       public         heap    keycloak    false            �            1259    16510    user_entity    TABLE     =  CREATE TABLE public.user_entity (
    id character varying(36) NOT NULL,
    email character varying(255),
    email_constraint character varying(255),
    email_verified boolean DEFAULT false NOT NULL,
    enabled boolean DEFAULT false NOT NULL,
    federation_link character varying(255),
    first_name character varying(255),
    last_name character varying(255),
    realm_id character varying(255),
    username character varying(255),
    created_timestamp bigint,
    service_account_client_link character varying(255),
    not_before integer DEFAULT 0 NOT NULL
);
    DROP TABLE public.user_entity;
       public         heap    keycloak    false            �            1259    16519    user_federation_config    TABLE     �   CREATE TABLE public.user_federation_config (
    user_federation_provider_id character varying(36) NOT NULL,
    value character varying(255),
    name character varying(255) NOT NULL
);
 *   DROP TABLE public.user_federation_config;
       public         heap    keycloak    false            �            1259    17073    user_federation_mapper    TABLE     $  CREATE TABLE public.user_federation_mapper (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    federation_provider_id character varying(36) NOT NULL,
    federation_mapper_type character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL
);
 *   DROP TABLE public.user_federation_mapper;
       public         heap    keycloak    false            �            1259    17079    user_federation_mapper_config    TABLE     �   CREATE TABLE public.user_federation_mapper_config (
    user_federation_mapper_id character varying(36) NOT NULL,
    value character varying(255),
    name character varying(255) NOT NULL
);
 1   DROP TABLE public.user_federation_mapper_config;
       public         heap    keycloak    false            �            1259    16525    user_federation_provider    TABLE     ;  CREATE TABLE public.user_federation_provider (
    id character varying(36) NOT NULL,
    changed_sync_period integer,
    display_name character varying(255),
    full_sync_period integer,
    last_sync integer,
    priority integer,
    provider_name character varying(255),
    realm_id character varying(36)
);
 ,   DROP TABLE public.user_federation_provider;
       public         heap    keycloak    false                       1259    17249    user_group_membership    TABLE     �   CREATE TABLE public.user_group_membership (
    group_id character varying(36) NOT NULL,
    user_id character varying(36) NOT NULL
);
 )   DROP TABLE public.user_group_membership;
       public         heap    keycloak    false            �            1259    16531    user_required_action    TABLE     �   CREATE TABLE public.user_required_action (
    user_id character varying(36) NOT NULL,
    required_action character varying(255) DEFAULT ' '::character varying NOT NULL
);
 (   DROP TABLE public.user_required_action;
       public         heap    keycloak    false            �            1259    16534    user_role_mapping    TABLE     �   CREATE TABLE public.user_role_mapping (
    role_id character varying(255) NOT NULL,
    user_id character varying(36) NOT NULL
);
 %   DROP TABLE public.user_role_mapping;
       public         heap    keycloak    false            �            1259    16537    user_session    TABLE     �  CREATE TABLE public.user_session (
    id character varying(36) NOT NULL,
    auth_method character varying(255),
    ip_address character varying(255),
    last_session_refresh integer,
    login_username character varying(255),
    realm_id character varying(255),
    remember_me boolean DEFAULT false NOT NULL,
    started integer,
    user_id character varying(255),
    user_session_state integer,
    broker_session_id character varying(255),
    broker_user_id character varying(255)
);
     DROP TABLE public.user_session;
       public         heap    keycloak    false            �            1259    16852    user_session_note    TABLE     �   CREATE TABLE public.user_session_note (
    user_session character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(2048)
);
 %   DROP TABLE public.user_session_note;
       public         heap    keycloak    false            �            1259    16501    username_login_failure    TABLE       CREATE TABLE public.username_login_failure (
    realm_id character varying(36) NOT NULL,
    username character varying(255) NOT NULL,
    failed_login_not_before integer,
    last_failure bigint,
    last_ip_failure character varying(255),
    num_failures integer
);
 *   DROP TABLE public.username_login_failure;
       public         heap    keycloak    false            �            1259    16550    web_origins    TABLE     }   CREATE TABLE public.web_origins (
    client_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);
    DROP TABLE public.web_origins;
       public         heap    keycloak    false                      0    17044    admin_event_entity 
   TABLE DATA           �   COPY public.admin_event_entity (id, admin_event_time, realm_id, operation_type, auth_realm_id, auth_client_id, auth_user_id, ip_address, resource_path, representation, error, resource_type) FROM stdin;
    public          keycloak    false    243   _�      ,          0    17507    associated_policy 
   TABLE DATA           L   COPY public.associated_policy (policy_id, associated_policy_id) FROM stdin;
    public          keycloak    false    272   |�                0    17062    authentication_execution 
   TABLE DATA           �   COPY public.authentication_execution (id, alias, authenticator, realm_id, flow_id, requirement, priority, authenticator_flow, auth_flow_id, auth_config) FROM stdin;
    public          keycloak    false    246   ��                0    17056    authentication_flow 
   TABLE DATA           q   COPY public.authentication_flow (id, alias, description, realm_id, provider_id, top_level, built_in) FROM stdin;
    public          keycloak    false    245   �                0    17050    authenticator_config 
   TABLE DATA           C   COPY public.authenticator_config (id, alias, realm_id) FROM stdin;
    public          keycloak    false    244   ݫ                0    17067    authenticator_config_entry 
   TABLE DATA           S   COPY public.authenticator_config_entry (authenticator_id, value, name) FROM stdin;
    public          keycloak    false    247   ��      -          0    17522    broker_link 
   TABLE DATA           �   COPY public.broker_link (identity_provider, storage_provider_id, realm_id, broker_user_id, broker_username, token, user_id) FROM stdin;
    public          keycloak    false    273   m�      �          0    16399    client 
   TABLE DATA           �  COPY public.client (id, enabled, full_scope_allowed, client_id, not_before, public_client, secret, base_url, bearer_only, management_url, surrogate_auth_required, realm_id, protocol, node_rereg_timeout, frontchannel_logout, consent_required, name, service_accounts_enabled, client_authenticator_type, root_url, description, registration_token, standard_flow_enabled, implicit_flow_enabled, direct_access_grants_enabled, always_display_in_console) FROM stdin;
    public          keycloak    false    203   ��      �          0    16773    client_attributes 
   TABLE DATA           C   COPY public.client_attributes (client_id, value, name) FROM stdin;
    public          keycloak    false    227   C�      8          0    17781    client_auth_flow_bindings 
   TABLE DATA           U   COPY public.client_auth_flow_bindings (client_id, flow_id, binding_name) FROM stdin;
    public          keycloak    false    284   m�      �          0    16396    client_default_roles 
   TABLE DATA           B   COPY public.client_default_roles (client_id, role_id) FROM stdin;
    public          keycloak    false    202   ��      7          0    17656    client_initial_access 
   TABLE DATA           n   COPY public.client_initial_access (id, realm_id, "timestamp", expiration, count, remaining_count) FROM stdin;
    public          keycloak    false    283   ;�                0    16785    client_node_registrations 
   TABLE DATA           K   COPY public.client_node_registrations (client_id, value, name) FROM stdin;
    public          keycloak    false    229   X�      !          0    17305    client_scope 
   TABLE DATA           Q   COPY public.client_scope (id, name, realm_id, description, protocol) FROM stdin;
    public          keycloak    false    261   u�      "          0    17320    client_scope_attributes 
   TABLE DATA           H   COPY public.client_scope_attributes (scope_id, value, name) FROM stdin;
    public          keycloak    false    262   ��      9          0    17823    client_scope_client 
   TABLE DATA           Q   COPY public.client_scope_client (client_id, scope_id, default_scope) FROM stdin;
    public          keycloak    false    285   ��      #          0    17326    client_scope_role_mapping 
   TABLE DATA           F   COPY public.client_scope_role_mapping (scope_id, role_id) FROM stdin;
    public          keycloak    false    263   ��      �          0    16411    client_session 
   TABLE DATA           �   COPY public.client_session (id, client_id, redirect_uri, state, "timestamp", session_id, auth_method, realm_id, auth_user_id, current_action) FROM stdin;
    public          keycloak    false    204   ��                0    17088    client_session_auth_status 
   TABLE DATA           [   COPY public.client_session_auth_status (authenticator, status, client_session) FROM stdin;
    public          keycloak    false    250   ��                 0    16779    client_session_note 
   TABLE DATA           J   COPY public.client_session_note (name, value, client_session) FROM stdin;
    public          keycloak    false    228   ��                0    16966    client_session_prot_mapper 
   TABLE DATA           X   COPY public.client_session_prot_mapper (protocol_mapper_id, client_session) FROM stdin;
    public          keycloak    false    242   �      �          0    16417    client_session_role 
   TABLE DATA           F   COPY public.client_session_role (role_id, client_session) FROM stdin;
    public          keycloak    false    205   5�                0    17169    client_user_session_note 
   TABLE DATA           O   COPY public.client_user_session_note (name, value, client_session) FROM stdin;
    public          keycloak    false    251   R�      5          0    17572 	   component 
   TABLE DATA           h   COPY public.component (id, name, parent_id, provider_id, provider_type, realm_id, sub_type) FROM stdin;
    public          keycloak    false    281   o�      4          0    17566    component_config 
   TABLE DATA           I   COPY public.component_config (id, component_id, name, value) FROM stdin;
    public          keycloak    false    280   !�      �          0    16420    composite_role 
   TABLE DATA           ?   COPY public.composite_role (composite, child_role) FROM stdin;
    public          keycloak    false    206   ��      �          0    16423 
   credential 
   TABLE DATA              COPY public.credential (id, salt, type, user_id, created_date, user_label, secret_data, credential_data, priority) FROM stdin;
    public          keycloak    false    207   ��      �          0    16390    databasechangelog 
   TABLE DATA           �   COPY public.databasechangelog (id, author, filename, dateexecuted, orderexecuted, exectype, md5sum, description, comments, tag, liquibase, contexts, labels, deployment_id) FROM stdin;
    public          keycloak    false    201   ��      �          0    16385    databasechangeloglock 
   TABLE DATA           R   COPY public.databasechangeloglock (id, locked, lockgranted, lockedby) FROM stdin;
    public          keycloak    false    200           :          0    17839    default_client_scope 
   TABLE DATA           Q   COPY public.default_client_scope (realm_id, scope_id, default_scope) FROM stdin;
    public          keycloak    false    286   N       �          0    16429    event_entity 
   TABLE DATA           �   COPY public.event_entity (id, client_id, details_json, error, ip_address, realm_id, session_id, event_time, type, user_id) FROM stdin;
    public          keycloak    false    208   %      .          0    17528    fed_user_attribute 
   TABLE DATA           e   COPY public.fed_user_attribute (id, name, user_id, realm_id, storage_provider_id, value) FROM stdin;
    public          keycloak    false    274   B      /          0    17534    fed_user_consent 
   TABLE DATA           �   COPY public.fed_user_consent (id, client_id, user_id, realm_id, storage_provider_id, created_date, last_updated_date, client_storage_provider, external_client_id) FROM stdin;
    public          keycloak    false    275   _      <          0    17865    fed_user_consent_cl_scope 
   TABLE DATA           N   COPY public.fed_user_consent_cl_scope (user_consent_id, scope_id) FROM stdin;
    public          keycloak    false    288   |      0          0    17543    fed_user_credential 
   TABLE DATA           �   COPY public.fed_user_credential (id, salt, type, created_date, user_id, realm_id, storage_provider_id, user_label, secret_data, credential_data, priority) FROM stdin;
    public          keycloak    false    276   �      1          0    17553    fed_user_group_membership 
   TABLE DATA           e   COPY public.fed_user_group_membership (group_id, user_id, realm_id, storage_provider_id) FROM stdin;
    public          keycloak    false    277   �      2          0    17556    fed_user_required_action 
   TABLE DATA           k   COPY public.fed_user_required_action (required_action, user_id, realm_id, storage_provider_id) FROM stdin;
    public          keycloak    false    278   �      3          0    17563    fed_user_role_mapping 
   TABLE DATA           `   COPY public.fed_user_role_mapping (role_id, user_id, realm_id, storage_provider_id) FROM stdin;
    public          keycloak    false    279   �                0    16823    federated_identity 
   TABLE DATA           �   COPY public.federated_identity (identity_provider, realm_id, federated_user_id, federated_username, token, user_id) FROM stdin;
    public          keycloak    false    232         6          0    17632    federated_user 
   TABLE DATA           K   COPY public.federated_user (id, storage_provider_id, realm_id) FROM stdin;
    public          keycloak    false    282   *                0    17242    group_attribute 
   TABLE DATA           D   COPY public.group_attribute (id, name, value, group_id) FROM stdin;
    public          keycloak    false    258   G                0    17239    group_role_mapping 
   TABLE DATA           ?   COPY public.group_role_mapping (role_id, group_id) FROM stdin;
    public          keycloak    false    257   d                0    16829    identity_provider 
   TABLE DATA             COPY public.identity_provider (internal_id, enabled, provider_alias, provider_id, store_token, authenticate_by_default, realm_id, add_token_role, trust_email, first_broker_login_flow_id, post_broker_login_flow_id, provider_display_name, link_only) FROM stdin;
    public          keycloak    false    233   �                0    16839    identity_provider_config 
   TABLE DATA           U   COPY public.identity_provider_config (identity_provider_id, value, name) FROM stdin;
    public          keycloak    false    234   �                0    16945    identity_provider_mapper 
   TABLE DATA           b   COPY public.identity_provider_mapper (id, name, idp_alias, idp_mapper_name, realm_id) FROM stdin;
    public          keycloak    false    239   �                0    16951    idp_mapper_config 
   TABLE DATA           G   COPY public.idp_mapper_config (idp_mapper_id, value, name) FROM stdin;
    public          keycloak    false    240   �                0    17236    keycloak_group 
   TABLE DATA           J   COPY public.keycloak_group (id, name, parent_group, realm_id) FROM stdin;
    public          keycloak    false    256   �      �          0    16438    keycloak_role 
   TABLE DATA           }   COPY public.keycloak_role (id, client_realm_constraint, client_role, description, name, realm_id, client, realm) FROM stdin;
    public          keycloak    false    209         
          0    16942    migration_model 
   TABLE DATA           C   COPY public.migration_model (id, version, update_time) FROM stdin;
    public          keycloak    false    238   �                0    17226    offline_client_session 
   TABLE DATA           �   COPY public.offline_client_session (user_session_id, client_id, offline_flag, "timestamp", data, client_storage_provider, external_client_id) FROM stdin;
    public          keycloak    false    255                    0    17220    offline_user_session 
   TABLE DATA           �   COPY public.offline_user_session (user_session_id, user_id, realm_id, created_on, offline_flag, data, last_session_refresh) FROM stdin;
    public          keycloak    false    254   =      (          0    17449    policy_config 
   TABLE DATA           ?   COPY public.policy_config (policy_id, name, value) FROM stdin;
    public          keycloak    false    268   Z                0    16810    protocol_mapper 
   TABLE DATA           o   COPY public.protocol_mapper (id, name, protocol, protocol_mapper_name, client_id, client_scope_id) FROM stdin;
    public          keycloak    false    230   w                0    16817    protocol_mapper_config 
   TABLE DATA           Q   COPY public.protocol_mapper_config (protocol_mapper_id, value, name) FROM stdin;
    public          keycloak    false    231         �          0    16445    realm 
   TABLE DATA             COPY public.realm (id, access_code_lifespan, user_action_lifespan, access_token_lifespan, account_theme, admin_theme, email_theme, enabled, events_enabled, events_expiration, login_theme, name, not_before, password_policy, registration_allowed, remember_me, reset_password_allowed, social, ssl_required, sso_idle_timeout, sso_max_lifespan, update_profile_on_soc_login, verify_email, master_admin_client, login_lifespan, internationalization_enabled, default_locale, reg_email_as_username, admin_events_enabled, admin_events_details_enabled, edit_username_allowed, otp_policy_counter, otp_policy_window, otp_policy_period, otp_policy_digits, otp_policy_alg, otp_policy_type, browser_flow, registration_flow, direct_grant_flow, reset_credentials_flow, client_auth_flow, offline_session_idle_timeout, revoke_refresh_token, access_token_life_implicit, login_with_email_allowed, duplicate_emails_allowed, docker_auth_flow, refresh_token_max_reuse, allow_user_managed_access, sso_max_lifespan_remember_me, sso_idle_timeout_remember_me) FROM stdin;
    public          keycloak    false    210   x"      �          0    16463    realm_attribute 
   TABLE DATA           @   COPY public.realm_attribute (name, value, realm_id) FROM stdin;
    public          keycloak    false    211   R$                 0    17252    realm_default_groups 
   TABLE DATA           B   COPY public.realm_default_groups (realm_id, group_id) FROM stdin;
    public          keycloak    false    260   o'      �          0    16469    realm_default_roles 
   TABLE DATA           @   COPY public.realm_default_roles (realm_id, role_id) FROM stdin;
    public          keycloak    false    212   �'      	          0    16934    realm_enabled_event_types 
   TABLE DATA           D   COPY public.realm_enabled_event_types (realm_id, value) FROM stdin;
    public          keycloak    false    237   '(      �          0    16472    realm_events_listeners 
   TABLE DATA           A   COPY public.realm_events_listeners (realm_id, value) FROM stdin;
    public          keycloak    false    213   D(      �          0    16475    realm_required_credential 
   TABLE DATA           ^   COPY public.realm_required_credential (type, form_label, input, secret, realm_id) FROM stdin;
    public          keycloak    false    214   �(      �          0    16483    realm_smtp_config 
   TABLE DATA           B   COPY public.realm_smtp_config (realm_id, value, name) FROM stdin;
    public          keycloak    false    215   �(                0    16849    realm_supported_locales 
   TABLE DATA           B   COPY public.realm_supported_locales (realm_id, value) FROM stdin;
    public          keycloak    false    235   <)      �          0    16495    redirect_uris 
   TABLE DATA           9   COPY public.redirect_uris (client_id, value) FROM stdin;
    public          keycloak    false    216   Y)                0    17183    required_action_config 
   TABLE DATA           Q   COPY public.required_action_config (required_action_id, value, name) FROM stdin;
    public          keycloak    false    253   �*                0    17175    required_action_provider 
   TABLE DATA           }   COPY public.required_action_provider (id, alias, name, realm_id, enabled, default_action, provider_id, priority) FROM stdin;
    public          keycloak    false    252   �*      >          0    17904    resource_attribute 
   TABLE DATA           J   COPY public.resource_attribute (id, name, value, resource_id) FROM stdin;
    public          keycloak    false    290   �,      *          0    17477    resource_policy 
   TABLE DATA           A   COPY public.resource_policy (resource_id, policy_id) FROM stdin;
    public          keycloak    false    270   -      )          0    17462    resource_scope 
   TABLE DATA           ?   COPY public.resource_scope (resource_id, scope_id) FROM stdin;
    public          keycloak    false    269   2-      $          0    17396    resource_server 
   TABLE DATA           k   COPY public.resource_server (id, allow_rs_remote_mgmt, policy_enforce_mode, decision_strategy) FROM stdin;
    public          keycloak    false    264   O-      =          0    17880    resource_server_perm_ticket 
   TABLE DATA           �   COPY public.resource_server_perm_ticket (id, owner, requester, created_timestamp, granted_timestamp, resource_id, scope_id, resource_server_id, policy_id) FROM stdin;
    public          keycloak    false    289   l-      '          0    17434    resource_server_policy 
   TABLE DATA           �   COPY public.resource_server_policy (id, name, description, type, decision_strategy, logic, resource_server_id, owner) FROM stdin;
    public          keycloak    false    267   �-      %          0    17404    resource_server_resource 
   TABLE DATA           �   COPY public.resource_server_resource (id, name, type, icon_uri, owner, resource_server_id, owner_managed_access, display_name) FROM stdin;
    public          keycloak    false    265   �-      &          0    17419    resource_server_scope 
   TABLE DATA           e   COPY public.resource_server_scope (id, name, icon_uri, resource_server_id, display_name) FROM stdin;
    public          keycloak    false    266   �-      ?          0    17923    resource_uris 
   TABLE DATA           ;   COPY public.resource_uris (resource_id, value) FROM stdin;
    public          keycloak    false    291   �-      @          0    17933    role_attribute 
   TABLE DATA           B   COPY public.role_attribute (id, role_id, name, value) FROM stdin;
    public          keycloak    false    292   �-      �          0    16498    scope_mapping 
   TABLE DATA           ;   COPY public.scope_mapping (client_id, role_id) FROM stdin;
    public          keycloak    false    217   .      +          0    17492    scope_policy 
   TABLE DATA           ;   COPY public.scope_policy (scope_id, policy_id) FROM stdin;
    public          keycloak    false    271   �.      �          0    16504    user_attribute 
   TABLE DATA           B   COPY public.user_attribute (name, value, user_id, id) FROM stdin;
    public          keycloak    false    219   /                0    16957    user_consent 
   TABLE DATA           �   COPY public.user_consent (id, client_id, user_id, created_date, last_updated_date, client_storage_provider, external_client_id) FROM stdin;
    public          keycloak    false    241   -/      ;          0    17855    user_consent_client_scope 
   TABLE DATA           N   COPY public.user_consent_client_scope (user_consent_id, scope_id) FROM stdin;
    public          keycloak    false    287   J/      �          0    16510    user_entity 
   TABLE DATA           �   COPY public.user_entity (id, email, email_constraint, email_verified, enabled, federation_link, first_name, last_name, realm_id, username, created_timestamp, service_account_client_link, not_before) FROM stdin;
    public          keycloak    false    220   g/      �          0    16519    user_federation_config 
   TABLE DATA           Z   COPY public.user_federation_config (user_federation_provider_id, value, name) FROM stdin;
    public          keycloak    false    221   1                0    17073    user_federation_mapper 
   TABLE DATA           t   COPY public.user_federation_mapper (id, name, federation_provider_id, federation_mapper_type, realm_id) FROM stdin;
    public          keycloak    false    248   11                0    17079    user_federation_mapper_config 
   TABLE DATA           _   COPY public.user_federation_mapper_config (user_federation_mapper_id, value, name) FROM stdin;
    public          keycloak    false    249   N1      �          0    16525    user_federation_provider 
   TABLE DATA           �   COPY public.user_federation_provider (id, changed_sync_period, display_name, full_sync_period, last_sync, priority, provider_name, realm_id) FROM stdin;
    public          keycloak    false    222   k1                0    17249    user_group_membership 
   TABLE DATA           B   COPY public.user_group_membership (group_id, user_id) FROM stdin;
    public          keycloak    false    259   �1      �          0    16531    user_required_action 
   TABLE DATA           H   COPY public.user_required_action (user_id, required_action) FROM stdin;
    public          keycloak    false    223   �1      �          0    16534    user_role_mapping 
   TABLE DATA           =   COPY public.user_role_mapping (role_id, user_id) FROM stdin;
    public          keycloak    false    224   �1      �          0    16537    user_session 
   TABLE DATA           �   COPY public.user_session (id, auth_method, ip_address, last_session_refresh, login_username, realm_id, remember_me, started, user_id, user_session_state, broker_session_id, broker_user_id) FROM stdin;
    public          keycloak    false    225   �3                0    16852    user_session_note 
   TABLE DATA           F   COPY public.user_session_note (user_session, name, value) FROM stdin;
    public          keycloak    false    236   �3      �          0    16501    username_login_failure 
   TABLE DATA           �   COPY public.username_login_failure (realm_id, username, failed_login_not_before, last_failure, last_ip_failure, num_failures) FROM stdin;
    public          keycloak    false    218   �3      �          0    16550    web_origins 
   TABLE DATA           7   COPY public.web_origins (client_id, value) FROM stdin;
    public          keycloak    false    226   
4                 2606    17646 &   username_login_failure CONSTRAINT_17-2 
   CONSTRAINT     v   ALTER TABLE ONLY public.username_login_failure
    ADD CONSTRAINT "CONSTRAINT_17-2" PRIMARY KEY (realm_id, username);
 R   ALTER TABLE ONLY public.username_login_failure DROP CONSTRAINT "CONSTRAINT_17-2";
       public            keycloak    false    218    218            �           2606    17960 ,   keycloak_role UK_J3RWUVD56ONTGSUHOGM184WW2-2 
   CONSTRAINT     �   ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT "UK_J3RWUVD56ONTGSUHOGM184WW2-2" UNIQUE (name, client_realm_constraint);
 X   ALTER TABLE ONLY public.keycloak_role DROP CONSTRAINT "UK_J3RWUVD56ONTGSUHOGM184WW2-2";
       public            keycloak    false    209    209            �           2606    17785 )   client_auth_flow_bindings c_cli_flow_bind 
   CONSTRAINT     |   ALTER TABLE ONLY public.client_auth_flow_bindings
    ADD CONSTRAINT c_cli_flow_bind PRIMARY KEY (client_id, binding_name);
 S   ALTER TABLE ONLY public.client_auth_flow_bindings DROP CONSTRAINT c_cli_flow_bind;
       public            keycloak    false    284    284            �           2606    17828 $   client_scope_client c_cli_scope_bind 
   CONSTRAINT     s   ALTER TABLE ONLY public.client_scope_client
    ADD CONSTRAINT c_cli_scope_bind PRIMARY KEY (client_id, scope_id);
 N   ALTER TABLE ONLY public.client_scope_client DROP CONSTRAINT c_cli_scope_bind;
       public            keycloak    false    285    285            �           2606    17660 .   client_initial_access cnstr_client_init_acc_pk 
   CONSTRAINT     l   ALTER TABLE ONLY public.client_initial_access
    ADD CONSTRAINT cnstr_client_init_acc_pk PRIMARY KEY (id);
 X   ALTER TABLE ONLY public.client_initial_access DROP CONSTRAINT cnstr_client_init_acc_pk;
       public            keycloak    false    283            �           2606    17291 ,   realm_default_groups con_group_id_def_groups 
   CONSTRAINT     k   ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT con_group_id_def_groups UNIQUE (group_id);
 V   ALTER TABLE ONLY public.realm_default_groups DROP CONSTRAINT con_group_id_def_groups;
       public            keycloak    false    260            �           2606    17579 !   broker_link constr_broker_link_pk 
   CONSTRAINT     w   ALTER TABLE ONLY public.broker_link
    ADD CONSTRAINT constr_broker_link_pk PRIMARY KEY (identity_provider, user_id);
 K   ALTER TABLE ONLY public.broker_link DROP CONSTRAINT constr_broker_link_pk;
       public            keycloak    false    273    273                       2606    17196 /   client_user_session_note constr_cl_usr_ses_note 
   CONSTRAINT        ALTER TABLE ONLY public.client_user_session_note
    ADD CONSTRAINT constr_cl_usr_ses_note PRIMARY KEY (client_session, name);
 Y   ALTER TABLE ONLY public.client_user_session_note DROP CONSTRAINT constr_cl_usr_ses_note;
       public            keycloak    false    251    251            �           2606    17754 0   client_default_roles constr_client_default_roles 
   CONSTRAINT     ~   ALTER TABLE ONLY public.client_default_roles
    ADD CONSTRAINT constr_client_default_roles PRIMARY KEY (client_id, role_id);
 Z   ALTER TABLE ONLY public.client_default_roles DROP CONSTRAINT constr_client_default_roles;
       public            keycloak    false    202    202            �           2606    17599 +   component_config constr_component_config_pk 
   CONSTRAINT     i   ALTER TABLE ONLY public.component_config
    ADD CONSTRAINT constr_component_config_pk PRIMARY KEY (id);
 U   ALTER TABLE ONLY public.component_config DROP CONSTRAINT constr_component_config_pk;
       public            keycloak    false    280            �           2606    17597    component constr_component_pk 
   CONSTRAINT     [   ALTER TABLE ONLY public.component
    ADD CONSTRAINT constr_component_pk PRIMARY KEY (id);
 G   ALTER TABLE ONLY public.component DROP CONSTRAINT constr_component_pk;
       public            keycloak    false    281            �           2606    17595 3   fed_user_required_action constr_fed_required_action 
   CONSTRAINT     �   ALTER TABLE ONLY public.fed_user_required_action
    ADD CONSTRAINT constr_fed_required_action PRIMARY KEY (required_action, user_id);
 ]   ALTER TABLE ONLY public.fed_user_required_action DROP CONSTRAINT constr_fed_required_action;
       public            keycloak    false    278    278            �           2606    17581 *   fed_user_attribute constr_fed_user_attr_pk 
   CONSTRAINT     h   ALTER TABLE ONLY public.fed_user_attribute
    ADD CONSTRAINT constr_fed_user_attr_pk PRIMARY KEY (id);
 T   ALTER TABLE ONLY public.fed_user_attribute DROP CONSTRAINT constr_fed_user_attr_pk;
       public            keycloak    false    274            �           2606    17583 +   fed_user_consent constr_fed_user_consent_pk 
   CONSTRAINT     i   ALTER TABLE ONLY public.fed_user_consent
    ADD CONSTRAINT constr_fed_user_consent_pk PRIMARY KEY (id);
 U   ALTER TABLE ONLY public.fed_user_consent DROP CONSTRAINT constr_fed_user_consent_pk;
       public            keycloak    false    275            �           2606    17589 +   fed_user_credential constr_fed_user_cred_pk 
   CONSTRAINT     i   ALTER TABLE ONLY public.fed_user_credential
    ADD CONSTRAINT constr_fed_user_cred_pk PRIMARY KEY (id);
 U   ALTER TABLE ONLY public.fed_user_credential DROP CONSTRAINT constr_fed_user_cred_pk;
       public            keycloak    false    276            �           2606    17591 /   fed_user_group_membership constr_fed_user_group 
   CONSTRAINT     |   ALTER TABLE ONLY public.fed_user_group_membership
    ADD CONSTRAINT constr_fed_user_group PRIMARY KEY (group_id, user_id);
 Y   ALTER TABLE ONLY public.fed_user_group_membership DROP CONSTRAINT constr_fed_user_group;
       public            keycloak    false    277    277            �           2606    17593 *   fed_user_role_mapping constr_fed_user_role 
   CONSTRAINT     v   ALTER TABLE ONLY public.fed_user_role_mapping
    ADD CONSTRAINT constr_fed_user_role PRIMARY KEY (role_id, user_id);
 T   ALTER TABLE ONLY public.fed_user_role_mapping DROP CONSTRAINT constr_fed_user_role;
       public            keycloak    false    279    279            �           2606    17639 $   federated_user constr_federated_user 
   CONSTRAINT     b   ALTER TABLE ONLY public.federated_user
    ADD CONSTRAINT constr_federated_user PRIMARY KEY (id);
 N   ALTER TABLE ONLY public.federated_user DROP CONSTRAINT constr_federated_user;
       public            keycloak    false    282            �           2606    17744 0   realm_default_groups constr_realm_default_groups 
   CONSTRAINT     ~   ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT constr_realm_default_groups PRIMARY KEY (realm_id, group_id);
 Z   ALTER TABLE ONLY public.realm_default_groups DROP CONSTRAINT constr_realm_default_groups;
       public            keycloak    false    260    260            W           2606    17761 8   realm_enabled_event_types constr_realm_enabl_event_types 
   CONSTRAINT     �   ALTER TABLE ONLY public.realm_enabled_event_types
    ADD CONSTRAINT constr_realm_enabl_event_types PRIMARY KEY (realm_id, value);
 b   ALTER TABLE ONLY public.realm_enabled_event_types DROP CONSTRAINT constr_realm_enabl_event_types;
       public            keycloak    false    237    237                       2606    17763 4   realm_events_listeners constr_realm_events_listeners 
   CONSTRAINT        ALTER TABLE ONLY public.realm_events_listeners
    ADD CONSTRAINT constr_realm_events_listeners PRIMARY KEY (realm_id, value);
 ^   ALTER TABLE ONLY public.realm_events_listeners DROP CONSTRAINT constr_realm_events_listeners;
       public            keycloak    false    213    213            R           2606    17765 6   realm_supported_locales constr_realm_supported_locales 
   CONSTRAINT     �   ALTER TABLE ONLY public.realm_supported_locales
    ADD CONSTRAINT constr_realm_supported_locales PRIMARY KEY (realm_id, value);
 `   ALTER TABLE ONLY public.realm_supported_locales DROP CONSTRAINT constr_realm_supported_locales;
       public            keycloak    false    235    235            K           2606    16862    identity_provider constraint_2b 
   CONSTRAINT     f   ALTER TABLE ONLY public.identity_provider
    ADD CONSTRAINT constraint_2b PRIMARY KEY (internal_id);
 I   ALTER TABLE ONLY public.identity_provider DROP CONSTRAINT constraint_2b;
       public            keycloak    false    233            ;           2606    16790    client_attributes constraint_3c 
   CONSTRAINT     j   ALTER TABLE ONLY public.client_attributes
    ADD CONSTRAINT constraint_3c PRIMARY KEY (client_id, name);
 I   ALTER TABLE ONLY public.client_attributes DROP CONSTRAINT constraint_3c;
       public            keycloak    false    227    227            �           2606    16562    event_entity constraint_4 
   CONSTRAINT     W   ALTER TABLE ONLY public.event_entity
    ADD CONSTRAINT constraint_4 PRIMARY KEY (id);
 C   ALTER TABLE ONLY public.event_entity DROP CONSTRAINT constraint_4;
       public            keycloak    false    208            G           2606    16864     federated_identity constraint_40 
   CONSTRAINT     v   ALTER TABLE ONLY public.federated_identity
    ADD CONSTRAINT constraint_40 PRIMARY KEY (identity_provider, user_id);
 J   ALTER TABLE ONLY public.federated_identity DROP CONSTRAINT constraint_40;
       public            keycloak    false    232    232                       2606    16564    realm constraint_4a 
   CONSTRAINT     Q   ALTER TABLE ONLY public.realm
    ADD CONSTRAINT constraint_4a PRIMARY KEY (id);
 =   ALTER TABLE ONLY public.realm DROP CONSTRAINT constraint_4a;
       public            keycloak    false    210            �           2606    16566     client_session_role constraint_5 
   CONSTRAINT     s   ALTER TABLE ONLY public.client_session_role
    ADD CONSTRAINT constraint_5 PRIMARY KEY (client_session, role_id);
 J   ALTER TABLE ONLY public.client_session_role DROP CONSTRAINT constraint_5;
       public            keycloak    false    205    205            6           2606    16568    user_session constraint_57 
   CONSTRAINT     X   ALTER TABLE ONLY public.user_session
    ADD CONSTRAINT constraint_57 PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.user_session DROP CONSTRAINT constraint_57;
       public            keycloak    false    225            -           2606    16570 &   user_federation_provider constraint_5c 
   CONSTRAINT     d   ALTER TABLE ONLY public.user_federation_provider
    ADD CONSTRAINT constraint_5c PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.user_federation_provider DROP CONSTRAINT constraint_5c;
       public            keycloak    false    222            =           2606    16792 !   client_session_note constraint_5e 
   CONSTRAINT     q   ALTER TABLE ONLY public.client_session_note
    ADD CONSTRAINT constraint_5e PRIMARY KEY (client_session, name);
 K   ALTER TABLE ONLY public.client_session_note DROP CONSTRAINT constraint_5e;
       public            keycloak    false    228    228            �           2606    16574    client constraint_7 
   CONSTRAINT     Q   ALTER TABLE ONLY public.client
    ADD CONSTRAINT constraint_7 PRIMARY KEY (id);
 =   ALTER TABLE ONLY public.client DROP CONSTRAINT constraint_7;
       public            keycloak    false    203            �           2606    16576    client_session constraint_8 
   CONSTRAINT     Y   ALTER TABLE ONLY public.client_session
    ADD CONSTRAINT constraint_8 PRIMARY KEY (id);
 E   ALTER TABLE ONLY public.client_session DROP CONSTRAINT constraint_8;
       public            keycloak    false    204                       2606    16578    scope_mapping constraint_81 
   CONSTRAINT     i   ALTER TABLE ONLY public.scope_mapping
    ADD CONSTRAINT constraint_81 PRIMARY KEY (client_id, role_id);
 E   ALTER TABLE ONLY public.scope_mapping DROP CONSTRAINT constraint_81;
       public            keycloak    false    217    217            ?           2606    16794 '   client_node_registrations constraint_84 
   CONSTRAINT     r   ALTER TABLE ONLY public.client_node_registrations
    ADD CONSTRAINT constraint_84 PRIMARY KEY (client_id, name);
 Q   ALTER TABLE ONLY public.client_node_registrations DROP CONSTRAINT constraint_84;
       public            keycloak    false    229    229            
           2606    16580    realm_attribute constraint_9 
   CONSTRAINT     f   ALTER TABLE ONLY public.realm_attribute
    ADD CONSTRAINT constraint_9 PRIMARY KEY (name, realm_id);
 F   ALTER TABLE ONLY public.realm_attribute DROP CONSTRAINT constraint_9;
       public            keycloak    false    211    211                       2606    16582 '   realm_required_credential constraint_92 
   CONSTRAINT     q   ALTER TABLE ONLY public.realm_required_credential
    ADD CONSTRAINT constraint_92 PRIMARY KEY (realm_id, type);
 Q   ALTER TABLE ONLY public.realm_required_credential DROP CONSTRAINT constraint_92;
       public            keycloak    false    214    214                       2606    16584    keycloak_role constraint_a 
   CONSTRAINT     X   ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT constraint_a PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.keycloak_role DROP CONSTRAINT constraint_a;
       public            keycloak    false    209            i           2606    17748 0   admin_event_entity constraint_admin_event_entity 
   CONSTRAINT     n   ALTER TABLE ONLY public.admin_event_entity
    ADD CONSTRAINT constraint_admin_event_entity PRIMARY KEY (id);
 Z   ALTER TABLE ONLY public.admin_event_entity DROP CONSTRAINT constraint_admin_event_entity;
       public            keycloak    false    243            u           2606    17101 1   authenticator_config_entry constraint_auth_cfg_pk 
   CONSTRAINT     �   ALTER TABLE ONLY public.authenticator_config_entry
    ADD CONSTRAINT constraint_auth_cfg_pk PRIMARY KEY (authenticator_id, name);
 [   ALTER TABLE ONLY public.authenticator_config_entry DROP CONSTRAINT constraint_auth_cfg_pk;
       public            keycloak    false    247    247            q           2606    17099 0   authentication_execution constraint_auth_exec_pk 
   CONSTRAINT     n   ALTER TABLE ONLY public.authentication_execution
    ADD CONSTRAINT constraint_auth_exec_pk PRIMARY KEY (id);
 Z   ALTER TABLE ONLY public.authentication_execution DROP CONSTRAINT constraint_auth_exec_pk;
       public            keycloak    false    246            n           2606    17097 +   authentication_flow constraint_auth_flow_pk 
   CONSTRAINT     i   ALTER TABLE ONLY public.authentication_flow
    ADD CONSTRAINT constraint_auth_flow_pk PRIMARY KEY (id);
 U   ALTER TABLE ONLY public.authentication_flow DROP CONSTRAINT constraint_auth_flow_pk;
       public            keycloak    false    245            k           2606    17095 '   authenticator_config constraint_auth_pk 
   CONSTRAINT     e   ALTER TABLE ONLY public.authenticator_config
    ADD CONSTRAINT constraint_auth_pk PRIMARY KEY (id);
 Q   ALTER TABLE ONLY public.authenticator_config DROP CONSTRAINT constraint_auth_pk;
       public            keycloak    false    244            }           2606    17105 4   client_session_auth_status constraint_auth_status_pk 
   CONSTRAINT     �   ALTER TABLE ONLY public.client_session_auth_status
    ADD CONSTRAINT constraint_auth_status_pk PRIMARY KEY (client_session, authenticator);
 ^   ALTER TABLE ONLY public.client_session_auth_status DROP CONSTRAINT constraint_auth_status_pk;
       public            keycloak    false    250    250            3           2606    16586    user_role_mapping constraint_c 
   CONSTRAINT     j   ALTER TABLE ONLY public.user_role_mapping
    ADD CONSTRAINT constraint_c PRIMARY KEY (role_id, user_id);
 H   ALTER TABLE ONLY public.user_role_mapping DROP CONSTRAINT constraint_c;
       public            keycloak    false    224    224            �           2606    17742 (   composite_role constraint_composite_role 
   CONSTRAINT     y   ALTER TABLE ONLY public.composite_role
    ADD CONSTRAINT constraint_composite_role PRIMARY KEY (composite, child_role);
 R   ALTER TABLE ONLY public.composite_role DROP CONSTRAINT constraint_composite_role;
       public            keycloak    false    206    206            g           2606    16982 /   client_session_prot_mapper constraint_cs_pmp_pk 
   CONSTRAINT     �   ALTER TABLE ONLY public.client_session_prot_mapper
    ADD CONSTRAINT constraint_cs_pmp_pk PRIMARY KEY (client_session, protocol_mapper_id);
 Y   ALTER TABLE ONLY public.client_session_prot_mapper DROP CONSTRAINT constraint_cs_pmp_pk;
       public            keycloak    false    242    242            P           2606    16866 %   identity_provider_config constraint_d 
   CONSTRAINT     {   ALTER TABLE ONLY public.identity_provider_config
    ADD CONSTRAINT constraint_d PRIMARY KEY (identity_provider_id, name);
 O   ALTER TABLE ONLY public.identity_provider_config DROP CONSTRAINT constraint_d;
       public            keycloak    false    234    234            �           2606    17456    policy_config constraint_dpc 
   CONSTRAINT     g   ALTER TABLE ONLY public.policy_config
    ADD CONSTRAINT constraint_dpc PRIMARY KEY (policy_id, name);
 F   ALTER TABLE ONLY public.policy_config DROP CONSTRAINT constraint_dpc;
       public            keycloak    false    268    268                       2606    16588    realm_smtp_config constraint_e 
   CONSTRAINT     h   ALTER TABLE ONLY public.realm_smtp_config
    ADD CONSTRAINT constraint_e PRIMARY KEY (realm_id, name);
 H   ALTER TABLE ONLY public.realm_smtp_config DROP CONSTRAINT constraint_e;
       public            keycloak    false    215    215            �           2606    16590    credential constraint_f 
   CONSTRAINT     U   ALTER TABLE ONLY public.credential
    ADD CONSTRAINT constraint_f PRIMARY KEY (id);
 A   ALTER TABLE ONLY public.credential DROP CONSTRAINT constraint_f;
       public            keycloak    false    207            +           2606    16592 $   user_federation_config constraint_f9 
   CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_config
    ADD CONSTRAINT constraint_f9 PRIMARY KEY (user_federation_provider_id, name);
 N   ALTER TABLE ONLY public.user_federation_config DROP CONSTRAINT constraint_f9;
       public            keycloak    false    221    221            �           2606    17884 ,   resource_server_perm_ticket constraint_fapmt 
   CONSTRAINT     j   ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT constraint_fapmt PRIMARY KEY (id);
 V   ALTER TABLE ONLY public.resource_server_perm_ticket DROP CONSTRAINT constraint_fapmt;
       public            keycloak    false    289            �           2606    17411 )   resource_server_resource constraint_farsr 
   CONSTRAINT     g   ALTER TABLE ONLY public.resource_server_resource
    ADD CONSTRAINT constraint_farsr PRIMARY KEY (id);
 S   ALTER TABLE ONLY public.resource_server_resource DROP CONSTRAINT constraint_farsr;
       public            keycloak    false    265            �           2606    17441 (   resource_server_policy constraint_farsrp 
   CONSTRAINT     f   ALTER TABLE ONLY public.resource_server_policy
    ADD CONSTRAINT constraint_farsrp PRIMARY KEY (id);
 R   ALTER TABLE ONLY public.resource_server_policy DROP CONSTRAINT constraint_farsrp;
       public            keycloak    false    267            �           2606    17511 %   associated_policy constraint_farsrpap 
   CONSTRAINT     �   ALTER TABLE ONLY public.associated_policy
    ADD CONSTRAINT constraint_farsrpap PRIMARY KEY (policy_id, associated_policy_id);
 O   ALTER TABLE ONLY public.associated_policy DROP CONSTRAINT constraint_farsrpap;
       public            keycloak    false    272    272            �           2606    17481 "   resource_policy constraint_farsrpp 
   CONSTRAINT     t   ALTER TABLE ONLY public.resource_policy
    ADD CONSTRAINT constraint_farsrpp PRIMARY KEY (resource_id, policy_id);
 L   ALTER TABLE ONLY public.resource_policy DROP CONSTRAINT constraint_farsrpp;
       public            keycloak    false    270    270            �           2606    17426 '   resource_server_scope constraint_farsrs 
   CONSTRAINT     e   ALTER TABLE ONLY public.resource_server_scope
    ADD CONSTRAINT constraint_farsrs PRIMARY KEY (id);
 Q   ALTER TABLE ONLY public.resource_server_scope DROP CONSTRAINT constraint_farsrs;
       public            keycloak    false    266            �           2606    17466 !   resource_scope constraint_farsrsp 
   CONSTRAINT     r   ALTER TABLE ONLY public.resource_scope
    ADD CONSTRAINT constraint_farsrsp PRIMARY KEY (resource_id, scope_id);
 K   ALTER TABLE ONLY public.resource_scope DROP CONSTRAINT constraint_farsrsp;
       public            keycloak    false    269    269            �           2606    17496     scope_policy constraint_farsrsps 
   CONSTRAINT     o   ALTER TABLE ONLY public.scope_policy
    ADD CONSTRAINT constraint_farsrsps PRIMARY KEY (scope_id, policy_id);
 J   ALTER TABLE ONLY public.scope_policy DROP CONSTRAINT constraint_farsrsps;
       public            keycloak    false    271    271            $           2606    16594    user_entity constraint_fb 
   CONSTRAINT     W   ALTER TABLE ONLY public.user_entity
    ADD CONSTRAINT constraint_fb PRIMARY KEY (id);
 C   ALTER TABLE ONLY public.user_entity DROP CONSTRAINT constraint_fb;
       public            keycloak    false    220            {           2606    17109 9   user_federation_mapper_config constraint_fedmapper_cfg_pm 
   CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_mapper_config
    ADD CONSTRAINT constraint_fedmapper_cfg_pm PRIMARY KEY (user_federation_mapper_id, name);
 c   ALTER TABLE ONLY public.user_federation_mapper_config DROP CONSTRAINT constraint_fedmapper_cfg_pm;
       public            keycloak    false    249    249            w           2606    17107 -   user_federation_mapper constraint_fedmapperpm 
   CONSTRAINT     k   ALTER TABLE ONLY public.user_federation_mapper
    ADD CONSTRAINT constraint_fedmapperpm PRIMARY KEY (id);
 W   ALTER TABLE ONLY public.user_federation_mapper DROP CONSTRAINT constraint_fedmapperpm;
       public            keycloak    false    248            �           2606    17869 6   fed_user_consent_cl_scope constraint_fgrntcsnt_clsc_pm 
   CONSTRAINT     �   ALTER TABLE ONLY public.fed_user_consent_cl_scope
    ADD CONSTRAINT constraint_fgrntcsnt_clsc_pm PRIMARY KEY (user_consent_id, scope_id);
 `   ALTER TABLE ONLY public.fed_user_consent_cl_scope DROP CONSTRAINT constraint_fgrntcsnt_clsc_pm;
       public            keycloak    false    288    288            �           2606    17859 5   user_consent_client_scope constraint_grntcsnt_clsc_pm 
   CONSTRAINT     �   ALTER TABLE ONLY public.user_consent_client_scope
    ADD CONSTRAINT constraint_grntcsnt_clsc_pm PRIMARY KEY (user_consent_id, scope_id);
 _   ALTER TABLE ONLY public.user_consent_client_scope DROP CONSTRAINT constraint_grntcsnt_clsc_pm;
       public            keycloak    false    287    287            b           2606    16976 #   user_consent constraint_grntcsnt_pm 
   CONSTRAINT     a   ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT constraint_grntcsnt_pm PRIMARY KEY (id);
 M   ALTER TABLE ONLY public.user_consent DROP CONSTRAINT constraint_grntcsnt_pm;
       public            keycloak    false    241            �           2606    17258    keycloak_group constraint_group 
   CONSTRAINT     ]   ALTER TABLE ONLY public.keycloak_group
    ADD CONSTRAINT constraint_group PRIMARY KEY (id);
 I   ALTER TABLE ONLY public.keycloak_group DROP CONSTRAINT constraint_group;
       public            keycloak    false    256            �           2606    17265 -   group_attribute constraint_group_attribute_pk 
   CONSTRAINT     k   ALTER TABLE ONLY public.group_attribute
    ADD CONSTRAINT constraint_group_attribute_pk PRIMARY KEY (id);
 W   ALTER TABLE ONLY public.group_attribute DROP CONSTRAINT constraint_group_attribute_pk;
       public            keycloak    false    258            �           2606    17279 (   group_role_mapping constraint_group_role 
   CONSTRAINT     u   ALTER TABLE ONLY public.group_role_mapping
    ADD CONSTRAINT constraint_group_role PRIMARY KEY (role_id, group_id);
 R   ALTER TABLE ONLY public.group_role_mapping DROP CONSTRAINT constraint_group_role;
       public            keycloak    false    257    257            ]           2606    16972 (   identity_provider_mapper constraint_idpm 
   CONSTRAINT     f   ALTER TABLE ONLY public.identity_provider_mapper
    ADD CONSTRAINT constraint_idpm PRIMARY KEY (id);
 R   ALTER TABLE ONLY public.identity_provider_mapper DROP CONSTRAINT constraint_idpm;
       public            keycloak    false    239            `           2606    17158 '   idp_mapper_config constraint_idpmconfig 
   CONSTRAINT     v   ALTER TABLE ONLY public.idp_mapper_config
    ADD CONSTRAINT constraint_idpmconfig PRIMARY KEY (idp_mapper_id, name);
 Q   ALTER TABLE ONLY public.idp_mapper_config DROP CONSTRAINT constraint_idpmconfig;
       public            keycloak    false    240    240            Z           2606    16970 !   migration_model constraint_migmod 
   CONSTRAINT     _   ALTER TABLE ONLY public.migration_model
    ADD CONSTRAINT constraint_migmod PRIMARY KEY (id);
 K   ALTER TABLE ONLY public.migration_model DROP CONSTRAINT constraint_migmod;
       public            keycloak    false    238            �           2606    17967 1   offline_client_session constraint_offl_cl_ses_pk3 
   CONSTRAINT     �   ALTER TABLE ONLY public.offline_client_session
    ADD CONSTRAINT constraint_offl_cl_ses_pk3 PRIMARY KEY (user_session_id, client_id, client_storage_provider, external_client_id, offline_flag);
 [   ALTER TABLE ONLY public.offline_client_session DROP CONSTRAINT constraint_offl_cl_ses_pk3;
       public            keycloak    false    255    255    255    255    255            �           2606    17233 /   offline_user_session constraint_offl_us_ses_pk2 
   CONSTRAINT     �   ALTER TABLE ONLY public.offline_user_session
    ADD CONSTRAINT constraint_offl_us_ses_pk2 PRIMARY KEY (user_session_id, offline_flag);
 Y   ALTER TABLE ONLY public.offline_user_session DROP CONSTRAINT constraint_offl_us_ses_pk2;
       public            keycloak    false    254    254            A           2606    16860    protocol_mapper constraint_pcm 
   CONSTRAINT     \   ALTER TABLE ONLY public.protocol_mapper
    ADD CONSTRAINT constraint_pcm PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.protocol_mapper DROP CONSTRAINT constraint_pcm;
       public            keycloak    false    230            E           2606    17151 *   protocol_mapper_config constraint_pmconfig 
   CONSTRAINT     ~   ALTER TABLE ONLY public.protocol_mapper_config
    ADD CONSTRAINT constraint_pmconfig PRIMARY KEY (protocol_mapper_id, name);
 T   ALTER TABLE ONLY public.protocol_mapper_config DROP CONSTRAINT constraint_pmconfig;
       public            keycloak    false    231    231                       2606    17740 2   realm_default_roles constraint_realm_default_roles 
   CONSTRAINT        ALTER TABLE ONLY public.realm_default_roles
    ADD CONSTRAINT constraint_realm_default_roles PRIMARY KEY (realm_id, role_id);
 \   ALTER TABLE ONLY public.realm_default_roles DROP CONSTRAINT constraint_realm_default_roles;
       public            keycloak    false    212    212                       2606    17767 &   redirect_uris constraint_redirect_uris 
   CONSTRAINT     r   ALTER TABLE ONLY public.redirect_uris
    ADD CONSTRAINT constraint_redirect_uris PRIMARY KEY (client_id, value);
 P   ALTER TABLE ONLY public.redirect_uris DROP CONSTRAINT constraint_redirect_uris;
       public            keycloak    false    216    216            �           2606    17194 0   required_action_config constraint_req_act_cfg_pk 
   CONSTRAINT     �   ALTER TABLE ONLY public.required_action_config
    ADD CONSTRAINT constraint_req_act_cfg_pk PRIMARY KEY (required_action_id, name);
 Z   ALTER TABLE ONLY public.required_action_config DROP CONSTRAINT constraint_req_act_cfg_pk;
       public            keycloak    false    253    253            �           2606    17192 2   required_action_provider constraint_req_act_prv_pk 
   CONSTRAINT     p   ALTER TABLE ONLY public.required_action_provider
    ADD CONSTRAINT constraint_req_act_prv_pk PRIMARY KEY (id);
 \   ALTER TABLE ONLY public.required_action_provider DROP CONSTRAINT constraint_req_act_prv_pk;
       public            keycloak    false    252            0           2606    17103 /   user_required_action constraint_required_action 
   CONSTRAINT     �   ALTER TABLE ONLY public.user_required_action
    ADD CONSTRAINT constraint_required_action PRIMARY KEY (required_action, user_id);
 Y   ALTER TABLE ONLY public.user_required_action DROP CONSTRAINT constraint_required_action;
       public            keycloak    false    223    223                       2606    17932 '   resource_uris constraint_resour_uris_pk 
   CONSTRAINT     u   ALTER TABLE ONLY public.resource_uris
    ADD CONSTRAINT constraint_resour_uris_pk PRIMARY KEY (resource_id, value);
 Q   ALTER TABLE ONLY public.resource_uris DROP CONSTRAINT constraint_resour_uris_pk;
       public            keycloak    false    291    291                       2606    17940 +   role_attribute constraint_role_attribute_pk 
   CONSTRAINT     i   ALTER TABLE ONLY public.role_attribute
    ADD CONSTRAINT constraint_role_attribute_pk PRIMARY KEY (id);
 U   ALTER TABLE ONLY public.role_attribute DROP CONSTRAINT constraint_role_attribute_pk;
       public            keycloak    false    292            !           2606    17190 +   user_attribute constraint_user_attribute_pk 
   CONSTRAINT     i   ALTER TABLE ONLY public.user_attribute
    ADD CONSTRAINT constraint_user_attribute_pk PRIMARY KEY (id);
 U   ALTER TABLE ONLY public.user_attribute DROP CONSTRAINT constraint_user_attribute_pk;
       public            keycloak    false    219            �           2606    17272 +   user_group_membership constraint_user_group 
   CONSTRAINT     x   ALTER TABLE ONLY public.user_group_membership
    ADD CONSTRAINT constraint_user_group PRIMARY KEY (group_id, user_id);
 U   ALTER TABLE ONLY public.user_group_membership DROP CONSTRAINT constraint_user_group;
       public            keycloak    false    259    259            U           2606    16870 #   user_session_note constraint_usn_pk 
   CONSTRAINT     q   ALTER TABLE ONLY public.user_session_note
    ADD CONSTRAINT constraint_usn_pk PRIMARY KEY (user_session, name);
 M   ALTER TABLE ONLY public.user_session_note DROP CONSTRAINT constraint_usn_pk;
       public            keycloak    false    236    236            8           2606    17769 "   web_origins constraint_web_origins 
   CONSTRAINT     n   ALTER TABLE ONLY public.web_origins
    ADD CONSTRAINT constraint_web_origins PRIMARY KEY (client_id, value);
 L   ALTER TABLE ONLY public.web_origins DROP CONSTRAINT constraint_web_origins;
       public            keycloak    false    226    226            �           2606    17377 '   client_scope_attributes pk_cl_tmpl_attr 
   CONSTRAINT     q   ALTER TABLE ONLY public.client_scope_attributes
    ADD CONSTRAINT pk_cl_tmpl_attr PRIMARY KEY (scope_id, name);
 Q   ALTER TABLE ONLY public.client_scope_attributes DROP CONSTRAINT pk_cl_tmpl_attr;
       public            keycloak    false    262    262            �           2606    17336    client_scope pk_cli_template 
   CONSTRAINT     Z   ALTER TABLE ONLY public.client_scope
    ADD CONSTRAINT pk_cli_template PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.client_scope DROP CONSTRAINT pk_cli_template;
       public            keycloak    false    261            �           2606    16389 .   databasechangeloglock pk_databasechangeloglock 
   CONSTRAINT     l   ALTER TABLE ONLY public.databasechangeloglock
    ADD CONSTRAINT pk_databasechangeloglock PRIMARY KEY (id);
 X   ALTER TABLE ONLY public.databasechangeloglock DROP CONSTRAINT pk_databasechangeloglock;
       public            keycloak    false    200            �           2606    17722 "   resource_server pk_resource_server 
   CONSTRAINT     `   ALTER TABLE ONLY public.resource_server
    ADD CONSTRAINT pk_resource_server PRIMARY KEY (id);
 L   ALTER TABLE ONLY public.resource_server DROP CONSTRAINT pk_resource_server;
       public            keycloak    false    264            �           2606    17365 +   client_scope_role_mapping pk_template_scope 
   CONSTRAINT     x   ALTER TABLE ONLY public.client_scope_role_mapping
    ADD CONSTRAINT pk_template_scope PRIMARY KEY (scope_id, role_id);
 U   ALTER TABLE ONLY public.client_scope_role_mapping DROP CONSTRAINT pk_template_scope;
       public            keycloak    false    263    263            �           2606    17844 )   default_client_scope r_def_cli_scope_bind 
   CONSTRAINT     w   ALTER TABLE ONLY public.default_client_scope
    ADD CONSTRAINT r_def_cli_scope_bind PRIMARY KEY (realm_id, scope_id);
 S   ALTER TABLE ONLY public.default_client_scope DROP CONSTRAINT r_def_cli_scope_bind;
       public            keycloak    false    286    286                       2606    17912    resource_attribute res_attr_pk 
   CONSTRAINT     \   ALTER TABLE ONLY public.resource_attribute
    ADD CONSTRAINT res_attr_pk PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.resource_attribute DROP CONSTRAINT res_attr_pk;
       public            keycloak    false    290            �           2606    17652    keycloak_group sibling_names 
   CONSTRAINT     o   ALTER TABLE ONLY public.keycloak_group
    ADD CONSTRAINT sibling_names UNIQUE (realm_id, parent_group, name);
 F   ALTER TABLE ONLY public.keycloak_group DROP CONSTRAINT sibling_names;
       public            keycloak    false    256    256    256            N           2606    16917 /   identity_provider uk_2daelwnibji49avxsrtuf6xj33 
   CONSTRAINT     ~   ALTER TABLE ONLY public.identity_provider
    ADD CONSTRAINT uk_2daelwnibji49avxsrtuf6xj33 UNIQUE (provider_alias, realm_id);
 Y   ALTER TABLE ONLY public.identity_provider DROP CONSTRAINT uk_2daelwnibji49avxsrtuf6xj33;
       public            keycloak    false    233    233            �           2606    16596 1   client_default_roles uk_8aelwnibji49avxsrtuf6xjow 
   CONSTRAINT     o   ALTER TABLE ONLY public.client_default_roles
    ADD CONSTRAINT uk_8aelwnibji49avxsrtuf6xjow UNIQUE (role_id);
 [   ALTER TABLE ONLY public.client_default_roles DROP CONSTRAINT uk_8aelwnibji49avxsrtuf6xjow;
       public            keycloak    false    202            �           2606    16598 #   client uk_b71cjlbenv945rb6gcon438at 
   CONSTRAINT     m   ALTER TABLE ONLY public.client
    ADD CONSTRAINT uk_b71cjlbenv945rb6gcon438at UNIQUE (realm_id, client_id);
 M   ALTER TABLE ONLY public.client DROP CONSTRAINT uk_b71cjlbenv945rb6gcon438at;
       public            keycloak    false    203    203            �           2606    17797    client_scope uk_cli_scope 
   CONSTRAINT     ^   ALTER TABLE ONLY public.client_scope
    ADD CONSTRAINT uk_cli_scope UNIQUE (realm_id, name);
 C   ALTER TABLE ONLY public.client_scope DROP CONSTRAINT uk_cli_scope;
       public            keycloak    false    261    261            '           2606    16602 (   user_entity uk_dykn684sl8up1crfei6eckhd7 
   CONSTRAINT     y   ALTER TABLE ONLY public.user_entity
    ADD CONSTRAINT uk_dykn684sl8up1crfei6eckhd7 UNIQUE (realm_id, email_constraint);
 R   ALTER TABLE ONLY public.user_entity DROP CONSTRAINT uk_dykn684sl8up1crfei6eckhd7;
       public            keycloak    false    220    220            �           2606    17976 4   resource_server_resource uk_frsr6t700s9v50bu18ws5ha6 
   CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_resource
    ADD CONSTRAINT uk_frsr6t700s9v50bu18ws5ha6 UNIQUE (name, owner, resource_server_id);
 ^   ALTER TABLE ONLY public.resource_server_resource DROP CONSTRAINT uk_frsr6t700s9v50bu18ws5ha6;
       public            keycloak    false    265    265    265                        2606    17971 7   resource_server_perm_ticket uk_frsr6t700s9v50bu18ws5pmt 
   CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT uk_frsr6t700s9v50bu18ws5pmt UNIQUE (owner, requester, resource_server_id, resource_id, scope_id);
 a   ALTER TABLE ONLY public.resource_server_perm_ticket DROP CONSTRAINT uk_frsr6t700s9v50bu18ws5pmt;
       public            keycloak    false    289    289    289    289    289            �           2606    17713 2   resource_server_policy uk_frsrpt700s9v50bu18ws5ha6 
   CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_policy
    ADD CONSTRAINT uk_frsrpt700s9v50bu18ws5ha6 UNIQUE (name, resource_server_id);
 \   ALTER TABLE ONLY public.resource_server_policy DROP CONSTRAINT uk_frsrpt700s9v50bu18ws5ha6;
       public            keycloak    false    267    267            �           2606    17717 1   resource_server_scope uk_frsrst700s9v50bu18ws5ha6 
   CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_scope
    ADD CONSTRAINT uk_frsrst700s9v50bu18ws5ha6 UNIQUE (name, resource_server_id);
 [   ALTER TABLE ONLY public.resource_server_scope DROP CONSTRAINT uk_frsrst700s9v50bu18ws5ha6;
       public            keycloak    false    266    266                       2606    16604 0   realm_default_roles uk_h4wpd7w4hsoolni3h0sw7btje 
   CONSTRAINT     n   ALTER TABLE ONLY public.realm_default_roles
    ADD CONSTRAINT uk_h4wpd7w4hsoolni3h0sw7btje UNIQUE (role_id);
 Z   ALTER TABLE ONLY public.realm_default_roles DROP CONSTRAINT uk_h4wpd7w4hsoolni3h0sw7btje;
       public            keycloak    false    212            e           2606    17962 )   user_consent uk_jkuwuvd56ontgsuhogm8uewrt 
   CONSTRAINT     �   ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT uk_jkuwuvd56ontgsuhogm8uewrt UNIQUE (client_id, client_storage_provider, external_client_id, user_id);
 S   ALTER TABLE ONLY public.user_consent DROP CONSTRAINT uk_jkuwuvd56ontgsuhogm8uewrt;
       public            keycloak    false    241    241    241    241                       2606    16610 "   realm uk_orvsdmla56612eaefiq6wl5oi 
   CONSTRAINT     ]   ALTER TABLE ONLY public.realm
    ADD CONSTRAINT uk_orvsdmla56612eaefiq6wl5oi UNIQUE (name);
 L   ALTER TABLE ONLY public.realm DROP CONSTRAINT uk_orvsdmla56612eaefiq6wl5oi;
       public            keycloak    false    210            )           2606    17641 (   user_entity uk_ru8tt6t700s9v50bu18ws5ha6 
   CONSTRAINT     q   ALTER TABLE ONLY public.user_entity
    ADD CONSTRAINT uk_ru8tt6t700s9v50bu18ws5ha6 UNIQUE (realm_id, username);
 R   ALTER TABLE ONLY public.user_entity DROP CONSTRAINT uk_ru8tt6t700s9v50bu18ws5ha6;
       public            keycloak    false    220    220            �           1259    17666    idx_assoc_pol_assoc_pol_id    INDEX     h   CREATE INDEX idx_assoc_pol_assoc_pol_id ON public.associated_policy USING btree (associated_policy_id);
 .   DROP INDEX public.idx_assoc_pol_assoc_pol_id;
       public            keycloak    false    272            l           1259    17670    idx_auth_config_realm    INDEX     Z   CREATE INDEX idx_auth_config_realm ON public.authenticator_config USING btree (realm_id);
 )   DROP INDEX public.idx_auth_config_realm;
       public            keycloak    false    244            r           1259    17668    idx_auth_exec_flow    INDEX     Z   CREATE INDEX idx_auth_exec_flow ON public.authentication_execution USING btree (flow_id);
 &   DROP INDEX public.idx_auth_exec_flow;
       public            keycloak    false    246            s           1259    17667    idx_auth_exec_realm_flow    INDEX     j   CREATE INDEX idx_auth_exec_realm_flow ON public.authentication_execution USING btree (realm_id, flow_id);
 ,   DROP INDEX public.idx_auth_exec_realm_flow;
       public            keycloak    false    246    246            o           1259    17669    idx_auth_flow_realm    INDEX     W   CREATE INDEX idx_auth_flow_realm ON public.authentication_flow USING btree (realm_id);
 '   DROP INDEX public.idx_auth_flow_realm;
       public            keycloak    false    245            �           1259    17876    idx_cl_clscope    INDEX     R   CREATE INDEX idx_cl_clscope ON public.client_scope_client USING btree (scope_id);
 "   DROP INDEX public.idx_cl_clscope;
       public            keycloak    false    285            �           1259    17672    idx_client_def_roles_client    INDEX     a   CREATE INDEX idx_client_def_roles_client ON public.client_default_roles USING btree (client_id);
 /   DROP INDEX public.idx_client_def_roles_client;
       public            keycloak    false    202            �           1259    17977    idx_client_id    INDEX     E   CREATE INDEX idx_client_id ON public.client USING btree (client_id);
 !   DROP INDEX public.idx_client_id;
       public            keycloak    false    203            �           1259    17710    idx_client_init_acc_realm    INDEX     _   CREATE INDEX idx_client_init_acc_realm ON public.client_initial_access USING btree (realm_id);
 -   DROP INDEX public.idx_client_init_acc_realm;
       public            keycloak    false    283            �           1259    17674    idx_client_session_session    INDEX     [   CREATE INDEX idx_client_session_session ON public.client_session USING btree (session_id);
 .   DROP INDEX public.idx_client_session_session;
       public            keycloak    false    204            �           1259    17874    idx_clscope_attrs    INDEX     Y   CREATE INDEX idx_clscope_attrs ON public.client_scope_attributes USING btree (scope_id);
 %   DROP INDEX public.idx_clscope_attrs;
       public            keycloak    false    262            �           1259    17875    idx_clscope_cl    INDEX     S   CREATE INDEX idx_clscope_cl ON public.client_scope_client USING btree (client_id);
 "   DROP INDEX public.idx_clscope_cl;
       public            keycloak    false    285            B           1259    17871    idx_clscope_protmap    INDEX     Z   CREATE INDEX idx_clscope_protmap ON public.protocol_mapper USING btree (client_scope_id);
 '   DROP INDEX public.idx_clscope_protmap;
       public            keycloak    false    230            �           1259    17872    idx_clscope_role    INDEX     Z   CREATE INDEX idx_clscope_role ON public.client_scope_role_mapping USING btree (scope_id);
 $   DROP INDEX public.idx_clscope_role;
       public            keycloak    false    263            �           1259    17676    idx_compo_config_compo    INDEX     [   CREATE INDEX idx_compo_config_compo ON public.component_config USING btree (component_id);
 *   DROP INDEX public.idx_compo_config_compo;
       public            keycloak    false    280            �           1259    17947    idx_component_provider_type    INDEX     Z   CREATE INDEX idx_component_provider_type ON public.component USING btree (provider_type);
 /   DROP INDEX public.idx_component_provider_type;
       public            keycloak    false    281            �           1259    17675    idx_component_realm    INDEX     M   CREATE INDEX idx_component_realm ON public.component USING btree (realm_id);
 '   DROP INDEX public.idx_component_realm;
       public            keycloak    false    281            �           1259    17677    idx_composite    INDEX     M   CREATE INDEX idx_composite ON public.composite_role USING btree (composite);
 !   DROP INDEX public.idx_composite;
       public            keycloak    false    206            �           1259    17678    idx_composite_child    INDEX     T   CREATE INDEX idx_composite_child ON public.composite_role USING btree (child_role);
 '   DROP INDEX public.idx_composite_child;
       public            keycloak    false    206            �           1259    17877    idx_defcls_realm    INDEX     U   CREATE INDEX idx_defcls_realm ON public.default_client_scope USING btree (realm_id);
 $   DROP INDEX public.idx_defcls_realm;
       public            keycloak    false    286            �           1259    17878    idx_defcls_scope    INDEX     U   CREATE INDEX idx_defcls_scope ON public.default_client_scope USING btree (scope_id);
 $   DROP INDEX public.idx_defcls_scope;
       public            keycloak    false    286            �           1259    17978    idx_event_time    INDEX     W   CREATE INDEX idx_event_time ON public.event_entity USING btree (realm_id, event_time);
 "   DROP INDEX public.idx_event_time;
       public            keycloak    false    208    208            H           1259    17395    idx_fedidentity_feduser    INDEX     c   CREATE INDEX idx_fedidentity_feduser ON public.federated_identity USING btree (federated_user_id);
 +   DROP INDEX public.idx_fedidentity_feduser;
       public            keycloak    false    232            I           1259    17394    idx_fedidentity_user    INDEX     V   CREATE INDEX idx_fedidentity_user ON public.federated_identity USING btree (user_id);
 (   DROP INDEX public.idx_fedidentity_user;
       public            keycloak    false    232            �           1259    17770    idx_fu_attribute    INDEX     b   CREATE INDEX idx_fu_attribute ON public.fed_user_attribute USING btree (user_id, realm_id, name);
 $   DROP INDEX public.idx_fu_attribute;
       public            keycloak    false    274    274    274            �           1259    17791    idx_fu_cnsnt_ext    INDEX     }   CREATE INDEX idx_fu_cnsnt_ext ON public.fed_user_consent USING btree (user_id, client_storage_provider, external_client_id);
 $   DROP INDEX public.idx_fu_cnsnt_ext;
       public            keycloak    false    275    275    275            �           1259    17958    idx_fu_consent    INDEX     Y   CREATE INDEX idx_fu_consent ON public.fed_user_consent USING btree (user_id, client_id);
 "   DROP INDEX public.idx_fu_consent;
       public            keycloak    false    275    275            �           1259    17772    idx_fu_consent_ru    INDEX     [   CREATE INDEX idx_fu_consent_ru ON public.fed_user_consent USING btree (realm_id, user_id);
 %   DROP INDEX public.idx_fu_consent_ru;
       public            keycloak    false    275    275            �           1259    17773    idx_fu_credential    INDEX     Z   CREATE INDEX idx_fu_credential ON public.fed_user_credential USING btree (user_id, type);
 %   DROP INDEX public.idx_fu_credential;
       public            keycloak    false    276    276            �           1259    17774    idx_fu_credential_ru    INDEX     a   CREATE INDEX idx_fu_credential_ru ON public.fed_user_credential USING btree (realm_id, user_id);
 (   DROP INDEX public.idx_fu_credential_ru;
       public            keycloak    false    276    276            �           1259    17775    idx_fu_group_membership    INDEX     j   CREATE INDEX idx_fu_group_membership ON public.fed_user_group_membership USING btree (user_id, group_id);
 +   DROP INDEX public.idx_fu_group_membership;
       public            keycloak    false    277    277            �           1259    17776    idx_fu_group_membership_ru    INDEX     m   CREATE INDEX idx_fu_group_membership_ru ON public.fed_user_group_membership USING btree (realm_id, user_id);
 .   DROP INDEX public.idx_fu_group_membership_ru;
       public            keycloak    false    277    277            �           1259    17777    idx_fu_required_action    INDEX     o   CREATE INDEX idx_fu_required_action ON public.fed_user_required_action USING btree (user_id, required_action);
 *   DROP INDEX public.idx_fu_required_action;
       public            keycloak    false    278    278            �           1259    17778    idx_fu_required_action_ru    INDEX     k   CREATE INDEX idx_fu_required_action_ru ON public.fed_user_required_action USING btree (realm_id, user_id);
 -   DROP INDEX public.idx_fu_required_action_ru;
       public            keycloak    false    278    278            �           1259    17779    idx_fu_role_mapping    INDEX     a   CREATE INDEX idx_fu_role_mapping ON public.fed_user_role_mapping USING btree (user_id, role_id);
 '   DROP INDEX public.idx_fu_role_mapping;
       public            keycloak    false    279    279            �           1259    17780    idx_fu_role_mapping_ru    INDEX     e   CREATE INDEX idx_fu_role_mapping_ru ON public.fed_user_role_mapping USING btree (realm_id, user_id);
 *   DROP INDEX public.idx_fu_role_mapping_ru;
       public            keycloak    false    279    279            �           1259    17681    idx_group_attr_group    INDEX     T   CREATE INDEX idx_group_attr_group ON public.group_attribute USING btree (group_id);
 (   DROP INDEX public.idx_group_attr_group;
       public            keycloak    false    258            �           1259    17682    idx_group_role_mapp_group    INDEX     \   CREATE INDEX idx_group_role_mapp_group ON public.group_role_mapping USING btree (group_id);
 -   DROP INDEX public.idx_group_role_mapp_group;
       public            keycloak    false    257            ^           1259    17684    idx_id_prov_mapp_realm    INDEX     _   CREATE INDEX idx_id_prov_mapp_realm ON public.identity_provider_mapper USING btree (realm_id);
 *   DROP INDEX public.idx_id_prov_mapp_realm;
       public            keycloak    false    239            L           1259    17683    idx_ident_prov_realm    INDEX     V   CREATE INDEX idx_ident_prov_realm ON public.identity_provider USING btree (realm_id);
 (   DROP INDEX public.idx_ident_prov_realm;
       public            keycloak    false    233                       1259    17685    idx_keycloak_role_client    INDEX     T   CREATE INDEX idx_keycloak_role_client ON public.keycloak_role USING btree (client);
 ,   DROP INDEX public.idx_keycloak_role_client;
       public            keycloak    false    209                       1259    17686    idx_keycloak_role_realm    INDEX     R   CREATE INDEX idx_keycloak_role_realm ON public.keycloak_role USING btree (realm);
 +   DROP INDEX public.idx_keycloak_role_realm;
       public            keycloak    false    209            �           1259    17951    idx_offline_uss_createdon    INDEX     `   CREATE INDEX idx_offline_uss_createdon ON public.offline_user_session USING btree (created_on);
 -   DROP INDEX public.idx_offline_uss_createdon;
       public            keycloak    false    254            C           1259    17687    idx_protocol_mapper_client    INDEX     [   CREATE INDEX idx_protocol_mapper_client ON public.protocol_mapper USING btree (client_id);
 .   DROP INDEX public.idx_protocol_mapper_client;
       public            keycloak    false    230                       1259    17690    idx_realm_attr_realm    INDEX     T   CREATE INDEX idx_realm_attr_realm ON public.realm_attribute USING btree (realm_id);
 (   DROP INDEX public.idx_realm_attr_realm;
       public            keycloak    false    211            �           1259    17870    idx_realm_clscope    INDEX     N   CREATE INDEX idx_realm_clscope ON public.client_scope USING btree (realm_id);
 %   DROP INDEX public.idx_realm_clscope;
       public            keycloak    false    261            �           1259    17691    idx_realm_def_grp_realm    INDEX     \   CREATE INDEX idx_realm_def_grp_realm ON public.realm_default_groups USING btree (realm_id);
 +   DROP INDEX public.idx_realm_def_grp_realm;
       public            keycloak    false    260                       1259    17692    idx_realm_def_roles_realm    INDEX     ]   CREATE INDEX idx_realm_def_roles_realm ON public.realm_default_roles USING btree (realm_id);
 -   DROP INDEX public.idx_realm_def_roles_realm;
       public            keycloak    false    212                       1259    17694    idx_realm_evt_list_realm    INDEX     _   CREATE INDEX idx_realm_evt_list_realm ON public.realm_events_listeners USING btree (realm_id);
 ,   DROP INDEX public.idx_realm_evt_list_realm;
       public            keycloak    false    213            X           1259    17693    idx_realm_evt_types_realm    INDEX     c   CREATE INDEX idx_realm_evt_types_realm ON public.realm_enabled_event_types USING btree (realm_id);
 -   DROP INDEX public.idx_realm_evt_types_realm;
       public            keycloak    false    237                       1259    17689    idx_realm_master_adm_cli    INDEX     Y   CREATE INDEX idx_realm_master_adm_cli ON public.realm USING btree (master_admin_client);
 ,   DROP INDEX public.idx_realm_master_adm_cli;
       public            keycloak    false    210            S           1259    17695    idx_realm_supp_local_realm    INDEX     b   CREATE INDEX idx_realm_supp_local_realm ON public.realm_supported_locales USING btree (realm_id);
 .   DROP INDEX public.idx_realm_supp_local_realm;
       public            keycloak    false    235                       1259    17696    idx_redir_uri_client    INDEX     S   CREATE INDEX idx_redir_uri_client ON public.redirect_uris USING btree (client_id);
 (   DROP INDEX public.idx_redir_uri_client;
       public            keycloak    false    216            �           1259    17697    idx_req_act_prov_realm    INDEX     _   CREATE INDEX idx_req_act_prov_realm ON public.required_action_provider USING btree (realm_id);
 *   DROP INDEX public.idx_req_act_prov_realm;
       public            keycloak    false    252            �           1259    17698    idx_res_policy_policy    INDEX     V   CREATE INDEX idx_res_policy_policy ON public.resource_policy USING btree (policy_id);
 )   DROP INDEX public.idx_res_policy_policy;
       public            keycloak    false    270            �           1259    17699    idx_res_scope_scope    INDEX     R   CREATE INDEX idx_res_scope_scope ON public.resource_scope USING btree (scope_id);
 '   DROP INDEX public.idx_res_scope_scope;
       public            keycloak    false    269            �           1259    17718    idx_res_serv_pol_res_serv    INDEX     j   CREATE INDEX idx_res_serv_pol_res_serv ON public.resource_server_policy USING btree (resource_server_id);
 -   DROP INDEX public.idx_res_serv_pol_res_serv;
       public            keycloak    false    267            �           1259    17719    idx_res_srv_res_res_srv    INDEX     j   CREATE INDEX idx_res_srv_res_res_srv ON public.resource_server_resource USING btree (resource_server_id);
 +   DROP INDEX public.idx_res_srv_res_res_srv;
       public            keycloak    false    265            �           1259    17720    idx_res_srv_scope_res_srv    INDEX     i   CREATE INDEX idx_res_srv_scope_res_srv ON public.resource_server_scope USING btree (resource_server_id);
 -   DROP INDEX public.idx_res_srv_scope_res_srv;
       public            keycloak    false    266                       1259    17946    idx_role_attribute    INDEX     P   CREATE INDEX idx_role_attribute ON public.role_attribute USING btree (role_id);
 &   DROP INDEX public.idx_role_attribute;
       public            keycloak    false    292            �           1259    17873    idx_role_clscope    INDEX     Y   CREATE INDEX idx_role_clscope ON public.client_scope_role_mapping USING btree (role_id);
 $   DROP INDEX public.idx_role_clscope;
       public            keycloak    false    263                       1259    17703    idx_scope_mapping_role    INDEX     S   CREATE INDEX idx_scope_mapping_role ON public.scope_mapping USING btree (role_id);
 *   DROP INDEX public.idx_scope_mapping_role;
       public            keycloak    false    217            �           1259    17704    idx_scope_policy_policy    INDEX     U   CREATE INDEX idx_scope_policy_policy ON public.scope_policy USING btree (policy_id);
 +   DROP INDEX public.idx_scope_policy_policy;
       public            keycloak    false    271            [           1259    17956    idx_update_time    INDEX     R   CREATE INDEX idx_update_time ON public.migration_model USING btree (update_time);
 #   DROP INDEX public.idx_update_time;
       public            keycloak    false    238            �           1259    17384    idx_us_sess_id_on_cl_sess    INDEX     g   CREATE INDEX idx_us_sess_id_on_cl_sess ON public.offline_client_session USING btree (user_session_id);
 -   DROP INDEX public.idx_us_sess_id_on_cl_sess;
       public            keycloak    false    255            �           1259    17879    idx_usconsent_clscope    INDEX     f   CREATE INDEX idx_usconsent_clscope ON public.user_consent_client_scope USING btree (user_consent_id);
 )   DROP INDEX public.idx_usconsent_clscope;
       public            keycloak    false    287            "           1259    17391    idx_user_attribute    INDEX     P   CREATE INDEX idx_user_attribute ON public.user_attribute USING btree (user_id);
 &   DROP INDEX public.idx_user_attribute;
       public            keycloak    false    219            c           1259    17388    idx_user_consent    INDEX     L   CREATE INDEX idx_user_consent ON public.user_consent USING btree (user_id);
 $   DROP INDEX public.idx_user_consent;
       public            keycloak    false    241            �           1259    17392    idx_user_credential    INDEX     M   CREATE INDEX idx_user_credential ON public.credential USING btree (user_id);
 '   DROP INDEX public.idx_user_credential;
       public            keycloak    false    207            %           1259    17385    idx_user_email    INDEX     G   CREATE INDEX idx_user_email ON public.user_entity USING btree (email);
 "   DROP INDEX public.idx_user_email;
       public            keycloak    false    220            �           1259    17387    idx_user_group_mapping    INDEX     [   CREATE INDEX idx_user_group_mapping ON public.user_group_membership USING btree (user_id);
 *   DROP INDEX public.idx_user_group_mapping;
       public            keycloak    false    259            1           1259    17393    idx_user_reqactions    INDEX     W   CREATE INDEX idx_user_reqactions ON public.user_required_action USING btree (user_id);
 '   DROP INDEX public.idx_user_reqactions;
       public            keycloak    false    223            4           1259    17386    idx_user_role_mapping    INDEX     V   CREATE INDEX idx_user_role_mapping ON public.user_role_mapping USING btree (user_id);
 )   DROP INDEX public.idx_user_role_mapping;
       public            keycloak    false    224            x           1259    17706    idx_usr_fed_map_fed_prv    INDEX     l   CREATE INDEX idx_usr_fed_map_fed_prv ON public.user_federation_mapper USING btree (federation_provider_id);
 +   DROP INDEX public.idx_usr_fed_map_fed_prv;
       public            keycloak    false    248            y           1259    17707    idx_usr_fed_map_realm    INDEX     \   CREATE INDEX idx_usr_fed_map_realm ON public.user_federation_mapper USING btree (realm_id);
 )   DROP INDEX public.idx_usr_fed_map_realm;
       public            keycloak    false    248            .           1259    17708    idx_usr_fed_prv_realm    INDEX     ^   CREATE INDEX idx_usr_fed_prv_realm ON public.user_federation_provider USING btree (realm_id);
 )   DROP INDEX public.idx_usr_fed_prv_realm;
       public            keycloak    false    222            9           1259    17709    idx_web_orig_client    INDEX     P   CREATE INDEX idx_web_orig_client ON public.web_origins USING btree (client_id);
 '   DROP INDEX public.idx_web_orig_client;
       public            keycloak    false    226            9           2606    17110 1   client_session_auth_status auth_status_constraint    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_session_auth_status
    ADD CONSTRAINT auth_status_constraint FOREIGN KEY (client_session) REFERENCES public.client_session(id);
 [   ALTER TABLE ONLY public.client_session_auth_status DROP CONSTRAINT auth_status_constraint;
       public          keycloak    false    3312    204    250            )           2606    16871 $   identity_provider fk2b4ebc52ae5c3b34    FK CONSTRAINT     �   ALTER TABLE ONLY public.identity_provider
    ADD CONSTRAINT fk2b4ebc52ae5c3b34 FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 N   ALTER TABLE ONLY public.identity_provider DROP CONSTRAINT fk2b4ebc52ae5c3b34;
       public          keycloak    false    233    210    3333            "           2606    16795 $   client_attributes fk3c47c64beacca966    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_attributes
    ADD CONSTRAINT fk3c47c64beacca966 FOREIGN KEY (client_id) REFERENCES public.client(id);
 N   ALTER TABLE ONLY public.client_attributes DROP CONSTRAINT fk3c47c64beacca966;
       public          keycloak    false    227    3307    203            (           2606    16881 %   federated_identity fk404288b92ef007a6    FK CONSTRAINT     �   ALTER TABLE ONLY public.federated_identity
    ADD CONSTRAINT fk404288b92ef007a6 FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 O   ALTER TABLE ONLY public.federated_identity DROP CONSTRAINT fk404288b92ef007a6;
       public          keycloak    false    220    3364    232            $           2606    17030 ,   client_node_registrations fk4129723ba992f594    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_node_registrations
    ADD CONSTRAINT fk4129723ba992f594 FOREIGN KEY (client_id) REFERENCES public.client(id);
 V   ALTER TABLE ONLY public.client_node_registrations DROP CONSTRAINT fk4129723ba992f594;
       public          keycloak    false    203    229    3307            #           2606    16800 &   client_session_note fk5edfb00ff51c2736    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_session_note
    ADD CONSTRAINT fk5edfb00ff51c2736 FOREIGN KEY (client_session) REFERENCES public.client_session(id);
 P   ALTER TABLE ONLY public.client_session_note DROP CONSTRAINT fk5edfb00ff51c2736;
       public          keycloak    false    228    3312    204            ,           2606    16911 $   user_session_note fk5edfb00ff51d3472    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_session_note
    ADD CONSTRAINT fk5edfb00ff51d3472 FOREIGN KEY (user_session) REFERENCES public.user_session(id);
 N   ALTER TABLE ONLY public.user_session_note DROP CONSTRAINT fk5edfb00ff51d3472;
       public          keycloak    false    225    236    3382                       2606    16613 0   client_session_role fk_11b7sgqw18i532811v7o2dv76    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_session_role
    ADD CONSTRAINT fk_11b7sgqw18i532811v7o2dv76 FOREIGN KEY (client_session) REFERENCES public.client_session(id);
 Z   ALTER TABLE ONLY public.client_session_role DROP CONSTRAINT fk_11b7sgqw18i532811v7o2dv76;
       public          keycloak    false    205    3312    204                       2606    16618 *   redirect_uris fk_1burs8pb4ouj97h5wuppahv9f    FK CONSTRAINT     �   ALTER TABLE ONLY public.redirect_uris
    ADD CONSTRAINT fk_1burs8pb4ouj97h5wuppahv9f FOREIGN KEY (client_id) REFERENCES public.client(id);
 T   ALTER TABLE ONLY public.redirect_uris DROP CONSTRAINT fk_1burs8pb4ouj97h5wuppahv9f;
       public          keycloak    false    216    3307    203                       2606    16623 5   user_federation_provider fk_1fj32f6ptolw2qy60cd8n01e8    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_provider
    ADD CONSTRAINT fk_1fj32f6ptolw2qy60cd8n01e8 FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 _   ALTER TABLE ONLY public.user_federation_provider DROP CONSTRAINT fk_1fj32f6ptolw2qy60cd8n01e8;
       public          keycloak    false    222    3333    210            1           2606    17008 7   client_session_prot_mapper fk_33a8sgqw18i532811v7o2dk89    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_session_prot_mapper
    ADD CONSTRAINT fk_33a8sgqw18i532811v7o2dk89 FOREIGN KEY (client_session) REFERENCES public.client_session(id);
 a   ALTER TABLE ONLY public.client_session_prot_mapper DROP CONSTRAINT fk_33a8sgqw18i532811v7o2dk89;
       public          keycloak    false    3312    204    242                       2606    16633 6   realm_required_credential fk_5hg65lybevavkqfki3kponh9v    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_required_credential
    ADD CONSTRAINT fk_5hg65lybevavkqfki3kponh9v FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 `   ALTER TABLE ONLY public.realm_required_credential DROP CONSTRAINT fk_5hg65lybevavkqfki3kponh9v;
       public          keycloak    false    214    3333    210            _           2606    17913 /   resource_attribute fk_5hrm2vlf9ql5fu022kqepovbr    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_attribute
    ADD CONSTRAINT fk_5hrm2vlf9ql5fu022kqepovbr FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);
 Y   ALTER TABLE ONLY public.resource_attribute DROP CONSTRAINT fk_5hrm2vlf9ql5fu022kqepovbr;
       public          keycloak    false    3500    265    290                       2606    16638 +   user_attribute fk_5hrm2vlf9ql5fu043kqepovbr    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_attribute
    ADD CONSTRAINT fk_5hrm2vlf9ql5fu043kqepovbr FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 U   ALTER TABLE ONLY public.user_attribute DROP CONSTRAINT fk_5hrm2vlf9ql5fu043kqepovbr;
       public          keycloak    false    219    220    3364                       2606    16648 1   user_required_action fk_6qj3w1jw9cvafhe19bwsiuvmd    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_required_action
    ADD CONSTRAINT fk_6qj3w1jw9cvafhe19bwsiuvmd FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 [   ALTER TABLE ONLY public.user_required_action DROP CONSTRAINT fk_6qj3w1jw9cvafhe19bwsiuvmd;
       public          keycloak    false    3364    220    223                       2606    16653 *   keycloak_role fk_6vyqfe4cn4wlq8r6kt5vdsj5c    FK CONSTRAINT     �   ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT fk_6vyqfe4cn4wlq8r6kt5vdsj5c FOREIGN KEY (realm) REFERENCES public.realm(id);
 T   ALTER TABLE ONLY public.keycloak_role DROP CONSTRAINT fk_6vyqfe4cn4wlq8r6kt5vdsj5c;
       public          keycloak    false    3333    210    209                       2606    16658 .   realm_smtp_config fk_70ej8xdxgxd0b9hh6180irr0o    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_smtp_config
    ADD CONSTRAINT fk_70ej8xdxgxd0b9hh6180irr0o FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 X   ALTER TABLE ONLY public.realm_smtp_config DROP CONSTRAINT fk_70ej8xdxgxd0b9hh6180irr0o;
       public          keycloak    false    215    3333    210                       2606    16668 1   client_default_roles fk_8aelwnibji49avxsrtuf6xjow    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_default_roles
    ADD CONSTRAINT fk_8aelwnibji49avxsrtuf6xjow FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);
 [   ALTER TABLE ONLY public.client_default_roles DROP CONSTRAINT fk_8aelwnibji49avxsrtuf6xjow;
       public          keycloak    false    3329    209    202                       2606    16673 ,   realm_attribute fk_8shxd6l3e9atqukacxgpffptw    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_attribute
    ADD CONSTRAINT fk_8shxd6l3e9atqukacxgpffptw FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 V   ALTER TABLE ONLY public.realm_attribute DROP CONSTRAINT fk_8shxd6l3e9atqukacxgpffptw;
       public          keycloak    false    211    210    3333                       2606    16678 +   composite_role fk_a63wvekftu8jo1pnj81e7mce2    FK CONSTRAINT     �   ALTER TABLE ONLY public.composite_role
    ADD CONSTRAINT fk_a63wvekftu8jo1pnj81e7mce2 FOREIGN KEY (composite) REFERENCES public.keycloak_role(id);
 U   ALTER TABLE ONLY public.composite_role DROP CONSTRAINT fk_a63wvekftu8jo1pnj81e7mce2;
       public          keycloak    false    206    3329    209            5           2606    17130 *   authentication_execution fk_auth_exec_flow    FK CONSTRAINT     �   ALTER TABLE ONLY public.authentication_execution
    ADD CONSTRAINT fk_auth_exec_flow FOREIGN KEY (flow_id) REFERENCES public.authentication_flow(id);
 T   ALTER TABLE ONLY public.authentication_execution DROP CONSTRAINT fk_auth_exec_flow;
       public          keycloak    false    3438    245    246            4           2606    17125 +   authentication_execution fk_auth_exec_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.authentication_execution
    ADD CONSTRAINT fk_auth_exec_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 U   ALTER TABLE ONLY public.authentication_execution DROP CONSTRAINT fk_auth_exec_realm;
       public          keycloak    false    210    3333    246            3           2606    17120 &   authentication_flow fk_auth_flow_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.authentication_flow
    ADD CONSTRAINT fk_auth_flow_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 P   ALTER TABLE ONLY public.authentication_flow DROP CONSTRAINT fk_auth_flow_realm;
       public          keycloak    false    245    3333    210            2           2606    17115 "   authenticator_config fk_auth_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.authenticator_config
    ADD CONSTRAINT fk_auth_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 L   ALTER TABLE ONLY public.authenticator_config DROP CONSTRAINT fk_auth_realm;
       public          keycloak    false    244    210    3333                       2606    16683 +   client_session fk_b4ao2vcvat6ukau74wbwtfqo1    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_session
    ADD CONSTRAINT fk_b4ao2vcvat6ukau74wbwtfqo1 FOREIGN KEY (session_id) REFERENCES public.user_session(id);
 U   ALTER TABLE ONLY public.client_session DROP CONSTRAINT fk_b4ao2vcvat6ukau74wbwtfqo1;
       public          keycloak    false    204    225    3382                        2606    16688 .   user_role_mapping fk_c4fqv34p1mbylloxang7b1q3l    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_role_mapping
    ADD CONSTRAINT fk_c4fqv34p1mbylloxang7b1q3l FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 X   ALTER TABLE ONLY public.user_role_mapping DROP CONSTRAINT fk_c4fqv34p1mbylloxang7b1q3l;
       public          keycloak    false    224    220    3364            V           2606    17829 )   client_scope_client fk_c_cli_scope_client    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_scope_client
    ADD CONSTRAINT fk_c_cli_scope_client FOREIGN KEY (client_id) REFERENCES public.client(id);
 S   ALTER TABLE ONLY public.client_scope_client DROP CONSTRAINT fk_c_cli_scope_client;
       public          keycloak    false    203    3307    285            W           2606    17834 (   client_scope_client fk_c_cli_scope_scope    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_scope_client
    ADD CONSTRAINT fk_c_cli_scope_scope FOREIGN KEY (scope_id) REFERENCES public.client_scope(id);
 R   ALTER TABLE ONLY public.client_scope_client DROP CONSTRAINT fk_c_cli_scope_scope;
       public          keycloak    false    261    3487    285            D           2606    17818 .   client_scope_attributes fk_cl_scope_attr_scope    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_scope_attributes
    ADD CONSTRAINT fk_cl_scope_attr_scope FOREIGN KEY (scope_id) REFERENCES public.client_scope(id);
 X   ALTER TABLE ONLY public.client_scope_attributes DROP CONSTRAINT fk_cl_scope_attr_scope;
       public          keycloak    false    3487    262    261            F           2606    17813 -   client_scope_role_mapping fk_cl_scope_rm_role    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_scope_role_mapping
    ADD CONSTRAINT fk_cl_scope_rm_role FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);
 W   ALTER TABLE ONLY public.client_scope_role_mapping DROP CONSTRAINT fk_cl_scope_rm_role;
       public          keycloak    false    209    3329    263            E           2606    17808 .   client_scope_role_mapping fk_cl_scope_rm_scope    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_scope_role_mapping
    ADD CONSTRAINT fk_cl_scope_rm_scope FOREIGN KEY (scope_id) REFERENCES public.client_scope(id);
 X   ALTER TABLE ONLY public.client_scope_role_mapping DROP CONSTRAINT fk_cl_scope_rm_scope;
       public          keycloak    false    263    3487    261            :           2606    17202 +   client_user_session_note fk_cl_usr_ses_note    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_user_session_note
    ADD CONSTRAINT fk_cl_usr_ses_note FOREIGN KEY (client_session) REFERENCES public.client_session(id);
 U   ALTER TABLE ONLY public.client_user_session_note DROP CONSTRAINT fk_cl_usr_ses_note;
       public          keycloak    false    3312    251    204            &           2606    17803 #   protocol_mapper fk_cli_scope_mapper    FK CONSTRAINT     �   ALTER TABLE ONLY public.protocol_mapper
    ADD CONSTRAINT fk_cli_scope_mapper FOREIGN KEY (client_scope_id) REFERENCES public.client_scope(id);
 M   ALTER TABLE ONLY public.protocol_mapper DROP CONSTRAINT fk_cli_scope_mapper;
       public          keycloak    false    3487    230    261            U           2606    17661 .   client_initial_access fk_client_init_acc_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_initial_access
    ADD CONSTRAINT fk_client_init_acc_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 X   ALTER TABLE ONLY public.client_initial_access DROP CONSTRAINT fk_client_init_acc_realm;
       public          keycloak    false    283    3333    210            S           2606    17605 $   component_config fk_component_config    FK CONSTRAINT     �   ALTER TABLE ONLY public.component_config
    ADD CONSTRAINT fk_component_config FOREIGN KEY (component_id) REFERENCES public.component(id);
 N   ALTER TABLE ONLY public.component_config DROP CONSTRAINT fk_component_config;
       public          keycloak    false    3558    280    281            T           2606    17600    component fk_component_realm    FK CONSTRAINT     |   ALTER TABLE ONLY public.component
    ADD CONSTRAINT fk_component_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 F   ALTER TABLE ONLY public.component DROP CONSTRAINT fk_component_realm;
       public          keycloak    false    3333    281    210            B           2606    17297 (   realm_default_groups fk_def_groups_group    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT fk_def_groups_group FOREIGN KEY (group_id) REFERENCES public.keycloak_group(id);
 R   ALTER TABLE ONLY public.realm_default_groups DROP CONSTRAINT fk_def_groups_group;
       public          keycloak    false    260    256    3468            A           2606    17292 (   realm_default_groups fk_def_groups_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT fk_def_groups_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 R   ALTER TABLE ONLY public.realm_default_groups DROP CONSTRAINT fk_def_groups_realm;
       public          keycloak    false    260    3333    210                       2606    16698 0   realm_default_roles fk_evudb1ppw84oxfax2drs03icc    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_default_roles
    ADD CONSTRAINT fk_evudb1ppw84oxfax2drs03icc FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 Z   ALTER TABLE ONLY public.realm_default_roles DROP CONSTRAINT fk_evudb1ppw84oxfax2drs03icc;
       public          keycloak    false    212    210    3333            8           2606    17145 .   user_federation_mapper_config fk_fedmapper_cfg    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_mapper_config
    ADD CONSTRAINT fk_fedmapper_cfg FOREIGN KEY (user_federation_mapper_id) REFERENCES public.user_federation_mapper(id);
 X   ALTER TABLE ONLY public.user_federation_mapper_config DROP CONSTRAINT fk_fedmapper_cfg;
       public          keycloak    false    3447    248    249            7           2606    17140 ,   user_federation_mapper fk_fedmapperpm_fedprv    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_mapper
    ADD CONSTRAINT fk_fedmapperpm_fedprv FOREIGN KEY (federation_provider_id) REFERENCES public.user_federation_provider(id);
 V   ALTER TABLE ONLY public.user_federation_mapper DROP CONSTRAINT fk_fedmapperpm_fedprv;
       public          keycloak    false    248    3373    222            6           2606    17135 +   user_federation_mapper fk_fedmapperpm_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_mapper
    ADD CONSTRAINT fk_fedmapperpm_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 U   ALTER TABLE ONLY public.user_federation_mapper DROP CONSTRAINT fk_fedmapperpm_realm;
       public          keycloak    false    248    210    3333            R           2606    17517 .   associated_policy fk_frsr5s213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.associated_policy
    ADD CONSTRAINT fk_frsr5s213xcx4wnkog82ssrfy FOREIGN KEY (associated_policy_id) REFERENCES public.resource_server_policy(id);
 X   ALTER TABLE ONLY public.associated_policy DROP CONSTRAINT fk_frsr5s213xcx4wnkog82ssrfy;
       public          keycloak    false    272    3510    267            P           2606    17502 )   scope_policy fk_frsrasp13xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.scope_policy
    ADD CONSTRAINT fk_frsrasp13xcx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);
 S   ALTER TABLE ONLY public.scope_policy DROP CONSTRAINT fk_frsrasp13xcx4wnkog82ssrfy;
       public          keycloak    false    271    3510    267            [           2606    17885 8   resource_server_perm_ticket fk_frsrho213xcx4wnkog82sspmt    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrho213xcx4wnkog82sspmt FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);
 b   ALTER TABLE ONLY public.resource_server_perm_ticket DROP CONSTRAINT fk_frsrho213xcx4wnkog82sspmt;
       public          keycloak    false    289    264    3498            G           2606    17728 5   resource_server_resource fk_frsrho213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_resource
    ADD CONSTRAINT fk_frsrho213xcx4wnkog82ssrfy FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);
 _   ALTER TABLE ONLY public.resource_server_resource DROP CONSTRAINT fk_frsrho213xcx4wnkog82ssrfy;
       public          keycloak    false    264    265    3498            \           2606    17890 8   resource_server_perm_ticket fk_frsrho213xcx4wnkog83sspmt    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrho213xcx4wnkog83sspmt FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);
 b   ALTER TABLE ONLY public.resource_server_perm_ticket DROP CONSTRAINT fk_frsrho213xcx4wnkog83sspmt;
       public          keycloak    false    289    3500    265            ]           2606    17895 8   resource_server_perm_ticket fk_frsrho213xcx4wnkog84sspmt    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrho213xcx4wnkog84sspmt FOREIGN KEY (scope_id) REFERENCES public.resource_server_scope(id);
 b   ALTER TABLE ONLY public.resource_server_perm_ticket DROP CONSTRAINT fk_frsrho213xcx4wnkog84sspmt;
       public          keycloak    false    3505    289    266            Q           2606    17512 .   associated_policy fk_frsrpas14xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.associated_policy
    ADD CONSTRAINT fk_frsrpas14xcx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);
 X   ALTER TABLE ONLY public.associated_policy DROP CONSTRAINT fk_frsrpas14xcx4wnkog82ssrfy;
       public          keycloak    false    3510    267    272            O           2606    17497 )   scope_policy fk_frsrpass3xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.scope_policy
    ADD CONSTRAINT fk_frsrpass3xcx4wnkog82ssrfy FOREIGN KEY (scope_id) REFERENCES public.resource_server_scope(id);
 S   ALTER TABLE ONLY public.scope_policy DROP CONSTRAINT fk_frsrpass3xcx4wnkog82ssrfy;
       public          keycloak    false    266    271    3505            ^           2606    17918 8   resource_server_perm_ticket fk_frsrpo2128cx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrpo2128cx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);
 b   ALTER TABLE ONLY public.resource_server_perm_ticket DROP CONSTRAINT fk_frsrpo2128cx4wnkog82ssrfy;
       public          keycloak    false    3510    267    289            I           2606    17723 3   resource_server_policy fk_frsrpo213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_policy
    ADD CONSTRAINT fk_frsrpo213xcx4wnkog82ssrfy FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);
 ]   ALTER TABLE ONLY public.resource_server_policy DROP CONSTRAINT fk_frsrpo213xcx4wnkog82ssrfy;
       public          keycloak    false    267    3498    264            K           2606    17467 +   resource_scope fk_frsrpos13xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_scope
    ADD CONSTRAINT fk_frsrpos13xcx4wnkog82ssrfy FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);
 U   ALTER TABLE ONLY public.resource_scope DROP CONSTRAINT fk_frsrpos13xcx4wnkog82ssrfy;
       public          keycloak    false    3500    265    269            M           2606    17482 ,   resource_policy fk_frsrpos53xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_policy
    ADD CONSTRAINT fk_frsrpos53xcx4wnkog82ssrfy FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);
 V   ALTER TABLE ONLY public.resource_policy DROP CONSTRAINT fk_frsrpos53xcx4wnkog82ssrfy;
       public          keycloak    false    270    265    3500            N           2606    17487 ,   resource_policy fk_frsrpp213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_policy
    ADD CONSTRAINT fk_frsrpp213xcx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);
 V   ALTER TABLE ONLY public.resource_policy DROP CONSTRAINT fk_frsrpp213xcx4wnkog82ssrfy;
       public          keycloak    false    3510    270    267            L           2606    17472 +   resource_scope fk_frsrps213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_scope
    ADD CONSTRAINT fk_frsrps213xcx4wnkog82ssrfy FOREIGN KEY (scope_id) REFERENCES public.resource_server_scope(id);
 U   ALTER TABLE ONLY public.resource_scope DROP CONSTRAINT fk_frsrps213xcx4wnkog82ssrfy;
       public          keycloak    false    269    3505    266            H           2606    17733 2   resource_server_scope fk_frsrso213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_server_scope
    ADD CONSTRAINT fk_frsrso213xcx4wnkog82ssrfy FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);
 \   ALTER TABLE ONLY public.resource_server_scope DROP CONSTRAINT fk_frsrso213xcx4wnkog82ssrfy;
       public          keycloak    false    266    264    3498                       2606    16703 +   composite_role fk_gr7thllb9lu8q4vqa4524jjy8    FK CONSTRAINT     �   ALTER TABLE ONLY public.composite_role
    ADD CONSTRAINT fk_gr7thllb9lu8q4vqa4524jjy8 FOREIGN KEY (child_role) REFERENCES public.keycloak_role(id);
 U   ALTER TABLE ONLY public.composite_role DROP CONSTRAINT fk_gr7thllb9lu8q4vqa4524jjy8;
       public          keycloak    false    206    209    3329            Z           2606    17860 .   user_consent_client_scope fk_grntcsnt_clsc_usc    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_consent_client_scope
    ADD CONSTRAINT fk_grntcsnt_clsc_usc FOREIGN KEY (user_consent_id) REFERENCES public.user_consent(id);
 X   ALTER TABLE ONLY public.user_consent_client_scope DROP CONSTRAINT fk_grntcsnt_clsc_usc;
       public          keycloak    false    287    3426    241            0           2606    16993    user_consent fk_grntcsnt_user    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT fk_grntcsnt_user FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 G   ALTER TABLE ONLY public.user_consent DROP CONSTRAINT fk_grntcsnt_user;
       public          keycloak    false    3364    220    241            ?           2606    17266 (   group_attribute fk_group_attribute_group    FK CONSTRAINT     �   ALTER TABLE ONLY public.group_attribute
    ADD CONSTRAINT fk_group_attribute_group FOREIGN KEY (group_id) REFERENCES public.keycloak_group(id);
 R   ALTER TABLE ONLY public.group_attribute DROP CONSTRAINT fk_group_attribute_group;
       public          keycloak    false    3468    258    256            <           2606    17259    keycloak_group fk_group_realm    FK CONSTRAINT     }   ALTER TABLE ONLY public.keycloak_group
    ADD CONSTRAINT fk_group_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 G   ALTER TABLE ONLY public.keycloak_group DROP CONSTRAINT fk_group_realm;
       public          keycloak    false    3333    256    210            =           2606    17280 &   group_role_mapping fk_group_role_group    FK CONSTRAINT     �   ALTER TABLE ONLY public.group_role_mapping
    ADD CONSTRAINT fk_group_role_group FOREIGN KEY (group_id) REFERENCES public.keycloak_group(id);
 P   ALTER TABLE ONLY public.group_role_mapping DROP CONSTRAINT fk_group_role_group;
       public          keycloak    false    3468    256    257            >           2606    17285 %   group_role_mapping fk_group_role_role    FK CONSTRAINT     �   ALTER TABLE ONLY public.group_role_mapping
    ADD CONSTRAINT fk_group_role_role FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);
 O   ALTER TABLE ONLY public.group_role_mapping DROP CONSTRAINT fk_group_role_role;
       public          keycloak    false    209    257    3329                       2606    16708 0   realm_default_roles fk_h4wpd7w4hsoolni3h0sw7btje    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_default_roles
    ADD CONSTRAINT fk_h4wpd7w4hsoolni3h0sw7btje FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);
 Z   ALTER TABLE ONLY public.realm_default_roles DROP CONSTRAINT fk_h4wpd7w4hsoolni3h0sw7btje;
       public          keycloak    false    212    209    3329            -           2606    16937 6   realm_enabled_event_types fk_h846o4h0w8epx5nwedrf5y69j    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_enabled_event_types
    ADD CONSTRAINT fk_h846o4h0w8epx5nwedrf5y69j FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 `   ALTER TABLE ONLY public.realm_enabled_event_types DROP CONSTRAINT fk_h846o4h0w8epx5nwedrf5y69j;
       public          keycloak    false    210    237    3333                       2606    16713 3   realm_events_listeners fk_h846o4h0w8epx5nxev9f5y69j    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_events_listeners
    ADD CONSTRAINT fk_h846o4h0w8epx5nxev9f5y69j FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 ]   ALTER TABLE ONLY public.realm_events_listeners DROP CONSTRAINT fk_h846o4h0w8epx5nxev9f5y69j;
       public          keycloak    false    213    210    3333            .           2606    16983 &   identity_provider_mapper fk_idpm_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.identity_provider_mapper
    ADD CONSTRAINT fk_idpm_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 P   ALTER TABLE ONLY public.identity_provider_mapper DROP CONSTRAINT fk_idpm_realm;
       public          keycloak    false    210    3333    239            /           2606    17159    idp_mapper_config fk_idpmconfig    FK CONSTRAINT     �   ALTER TABLE ONLY public.idp_mapper_config
    ADD CONSTRAINT fk_idpmconfig FOREIGN KEY (idp_mapper_id) REFERENCES public.identity_provider_mapper(id);
 I   ALTER TABLE ONLY public.idp_mapper_config DROP CONSTRAINT fk_idpmconfig;
       public          keycloak    false    3421    239    240                       2606    17035 *   keycloak_role fk_kjho5le2c0ral09fl8cm9wfw9    FK CONSTRAINT     �   ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT fk_kjho5le2c0ral09fl8cm9wfw9 FOREIGN KEY (client) REFERENCES public.client(id);
 T   ALTER TABLE ONLY public.keycloak_role DROP CONSTRAINT fk_kjho5le2c0ral09fl8cm9wfw9;
       public          keycloak    false    209    3307    203            !           2606    16723 (   web_origins fk_lojpho213xcx4wnkog82ssrfy    FK CONSTRAINT     �   ALTER TABLE ONLY public.web_origins
    ADD CONSTRAINT fk_lojpho213xcx4wnkog82ssrfy FOREIGN KEY (client_id) REFERENCES public.client(id);
 R   ALTER TABLE ONLY public.web_origins DROP CONSTRAINT fk_lojpho213xcx4wnkog82ssrfy;
       public          keycloak    false    226    203    3307            	           2606    17755 1   client_default_roles fk_nuilts7klwqw2h8m2b5joytky    FK CONSTRAINT     �   ALTER TABLE ONLY public.client_default_roles
    ADD CONSTRAINT fk_nuilts7klwqw2h8m2b5joytky FOREIGN KEY (client_id) REFERENCES public.client(id);
 [   ALTER TABLE ONLY public.client_default_roles DROP CONSTRAINT fk_nuilts7klwqw2h8m2b5joytky;
       public          keycloak    false    202    203    3307                       2606    16733 *   scope_mapping fk_ouse064plmlr732lxjcn1q5f1    FK CONSTRAINT     �   ALTER TABLE ONLY public.scope_mapping
    ADD CONSTRAINT fk_ouse064plmlr732lxjcn1q5f1 FOREIGN KEY (client_id) REFERENCES public.client(id);
 T   ALTER TABLE ONLY public.scope_mapping DROP CONSTRAINT fk_ouse064plmlr732lxjcn1q5f1;
       public          keycloak    false    217    203    3307                       2606    16738 *   scope_mapping fk_p3rh9grku11kqfrs4fltt7rnq    FK CONSTRAINT     �   ALTER TABLE ONLY public.scope_mapping
    ADD CONSTRAINT fk_p3rh9grku11kqfrs4fltt7rnq FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);
 T   ALTER TABLE ONLY public.scope_mapping DROP CONSTRAINT fk_p3rh9grku11kqfrs4fltt7rnq;
       public          keycloak    false    209    217    3329            
           2606    16743 #   client fk_p56ctinxxb9gsk57fo49f9tac    FK CONSTRAINT     �   ALTER TABLE ONLY public.client
    ADD CONSTRAINT fk_p56ctinxxb9gsk57fo49f9tac FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 M   ALTER TABLE ONLY public.client DROP CONSTRAINT fk_p56ctinxxb9gsk57fo49f9tac;
       public          keycloak    false    210    203    3333            %           2606    16876    protocol_mapper fk_pcm_realm    FK CONSTRAINT     ~   ALTER TABLE ONLY public.protocol_mapper
    ADD CONSTRAINT fk_pcm_realm FOREIGN KEY (client_id) REFERENCES public.client(id);
 F   ALTER TABLE ONLY public.protocol_mapper DROP CONSTRAINT fk_pcm_realm;
       public          keycloak    false    203    3307    230                       2606    16748 '   credential fk_pfyr0glasqyl0dei3kl69r6v0    FK CONSTRAINT     �   ALTER TABLE ONLY public.credential
    ADD CONSTRAINT fk_pfyr0glasqyl0dei3kl69r6v0 FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 Q   ALTER TABLE ONLY public.credential DROP CONSTRAINT fk_pfyr0glasqyl0dei3kl69r6v0;
       public          keycloak    false    3364    207    220            '           2606    17152 "   protocol_mapper_config fk_pmconfig    FK CONSTRAINT     �   ALTER TABLE ONLY public.protocol_mapper_config
    ADD CONSTRAINT fk_pmconfig FOREIGN KEY (protocol_mapper_id) REFERENCES public.protocol_mapper(id);
 L   ALTER TABLE ONLY public.protocol_mapper_config DROP CONSTRAINT fk_pmconfig;
       public          keycloak    false    230    231    3393            X           2606    17845 -   default_client_scope fk_r_def_cli_scope_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.default_client_scope
    ADD CONSTRAINT fk_r_def_cli_scope_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 W   ALTER TABLE ONLY public.default_client_scope DROP CONSTRAINT fk_r_def_cli_scope_realm;
       public          keycloak    false    210    286    3333            Y           2606    17850 -   default_client_scope fk_r_def_cli_scope_scope    FK CONSTRAINT     �   ALTER TABLE ONLY public.default_client_scope
    ADD CONSTRAINT fk_r_def_cli_scope_scope FOREIGN KEY (scope_id) REFERENCES public.client_scope(id);
 W   ALTER TABLE ONLY public.default_client_scope DROP CONSTRAINT fk_r_def_cli_scope_scope;
       public          keycloak    false    3487    286    261            C           2606    17798    client_scope fk_realm_cli_scope    FK CONSTRAINT        ALTER TABLE ONLY public.client_scope
    ADD CONSTRAINT fk_realm_cli_scope FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 I   ALTER TABLE ONLY public.client_scope DROP CONSTRAINT fk_realm_cli_scope;
       public          keycloak    false    3333    261    210            ;           2606    17197 )   required_action_provider fk_req_act_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.required_action_provider
    ADD CONSTRAINT fk_req_act_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 S   ALTER TABLE ONLY public.required_action_provider DROP CONSTRAINT fk_req_act_realm;
       public          keycloak    false    3333    210    252            `           2606    17926 %   resource_uris fk_resource_server_uris    FK CONSTRAINT     �   ALTER TABLE ONLY public.resource_uris
    ADD CONSTRAINT fk_resource_server_uris FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);
 O   ALTER TABLE ONLY public.resource_uris DROP CONSTRAINT fk_resource_server_uris;
       public          keycloak    false    265    3500    291            a           2606    17941 #   role_attribute fk_role_attribute_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.role_attribute
    ADD CONSTRAINT fk_role_attribute_id FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);
 M   ALTER TABLE ONLY public.role_attribute DROP CONSTRAINT fk_role_attribute_id;
       public          keycloak    false    292    3329    209            +           2606    16906 2   realm_supported_locales fk_supported_locales_realm    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm_supported_locales
    ADD CONSTRAINT fk_supported_locales_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);
 \   ALTER TABLE ONLY public.realm_supported_locales DROP CONSTRAINT fk_supported_locales_realm;
       public          keycloak    false    235    3333    210                       2606    16768 3   user_federation_config fk_t13hpu1j94r2ebpekr39x5eu5    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_federation_config
    ADD CONSTRAINT fk_t13hpu1j94r2ebpekr39x5eu5 FOREIGN KEY (user_federation_provider_id) REFERENCES public.user_federation_provider(id);
 ]   ALTER TABLE ONLY public.user_federation_config DROP CONSTRAINT fk_t13hpu1j94r2ebpekr39x5eu5;
       public          keycloak    false    221    3373    222                       2606    17015 "   realm fk_traf444kk6qrkms7n56aiwq5y    FK CONSTRAINT     �   ALTER TABLE ONLY public.realm
    ADD CONSTRAINT fk_traf444kk6qrkms7n56aiwq5y FOREIGN KEY (master_admin_client) REFERENCES public.client(id);
 L   ALTER TABLE ONLY public.realm DROP CONSTRAINT fk_traf444kk6qrkms7n56aiwq5y;
       public          keycloak    false    203    3307    210            @           2606    17273 (   user_group_membership fk_user_group_user    FK CONSTRAINT     �   ALTER TABLE ONLY public.user_group_membership
    ADD CONSTRAINT fk_user_group_user FOREIGN KEY (user_id) REFERENCES public.user_entity(id);
 R   ALTER TABLE ONLY public.user_group_membership DROP CONSTRAINT fk_user_group_user;
       public          keycloak    false    220    3364    259            J           2606    17457 !   policy_config fkdc34197cf864c4e43    FK CONSTRAINT     �   ALTER TABLE ONLY public.policy_config
    ADD CONSTRAINT fkdc34197cf864c4e43 FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);
 K   ALTER TABLE ONLY public.policy_config DROP CONSTRAINT fkdc34197cf864c4e43;
       public          keycloak    false    268    267    3510            *           2606    16886 +   identity_provider_config fkdc4897cf864c4e43    FK CONSTRAINT     �   ALTER TABLE ONLY public.identity_provider_config
    ADD CONSTRAINT fkdc4897cf864c4e43 FOREIGN KEY (identity_provider_id) REFERENCES public.identity_provider(internal_id);
 U   ALTER TABLE ONLY public.identity_provider_config DROP CONSTRAINT fkdc4897cf864c4e43;
       public          keycloak    false    233    234    3403                  x������ � �      ,      x������ � �            x��Z[rǮ�&�R7��X��9+�
ؼ�I�����I���nR���"��g�
$2Q=[	���e��qpE���Z�o~��}{������wr�==��w�a���J�ra�hPv1J�1�B��ě�oԾ��VOS|�.�]���	`<���]����˯�K7�
6C�2{pUzsYGu�Bq�Ҫ��V����?�=��}}|��<�GYw������j,o�a�党4���g77Wt��µ����Ob$�|��^Buk$ry���u����m�ʶ�V{f��u�'�Ƿ+�K���p�R�[��=��=�!�+==���><��S�~�9hj���u�֌����|GlV���v��7=�5�f�M.䆶�$�\q���U��G*s8�V��Fp�����[j�~�_w�w��e���^�~��dx]�)�Ch�	�g��w��8��c��)������!�'�v)�u��2��꫇�h6uBԫ�1�G/��~}��g�'}�[�,o����2NC�p1$;� ��O<%�(��q����1�����&���)u�q!��㿦�'�w�_��"m�����Ru�Dļ��#����w
#\�.Υ�}r��͂ݥ��+����X1ݎ&=���\	�:���$��\���w
�p5q�,�+�.��]+���
2��������E��ECp�;(��p�HPx	�����n�׻��G�����C�7
��XK÷,fG�蹖�j�5�e��7k�R�qI���њN��hָ�`��k��zg|g_�Q���#O��X��He��uu�Qk� ��Y6���
Ҝ$���Ǩ<>��q�� <��cjeRt#&�wPDD�l�Ps�����#��+Gc��4쾂Ԁ�A�u�xQ��HK��̿�?�O7�
�KC���iĖ�'�+bQԎ�*S��'y��ބ }yr����Ӆ��Xc��n�t�˔P��h$����;2��r���Q@�>A.�
���7�߃w��}�$�.��H+u-^V@H#�(�&�ܻ�ݶߓ�I��;eM8��XF���Ty1�j����{
#XC餞[͵���5AG�X�.dMq��R�	�Nȵ$X�չ��1Q�CN��)��Q��҈-�4��r�(�B	�-�Q��4��ɞ�ڝn���R���PX-(��˜
�������������ݮ$DnP�T;V�lE�Q��2��,.*2�p�F(
]Kk�>�����Y���8�"tѳ�'VV7��4�I�������.wCO�� 2H/�W�bCB�����?>	��t8Ѓsr�t��J&B'A�H䜫^�W���w��kxŜl*�5�U��� �qGm���]f�_��;a<�%4d�07C���4z[���������id������h��=��M�+�L���� \�u��������!(u���r1�<��d�����|��֓/����}������������Z�8K�5O4��� 7�T�	F��qTiO&��8&O�8���\�����6�C8W��)(��"6?��d%�m���娖�(���՜�d.����H��������z�h�ҤC�w:�'Nס��]�|y'�Obl����>[�+<Y։>5�$�g�k�S�>Eʂ���mB�Sa��Jp~��{k��Oy��;>�Sh�S�:êC�F|�`+:�|@��L*���8���>�$��:$|��	��|��{��F�"(i4�->Yq�G�~Nٞ�v��x�S���Ӝv���`f�jV�]�����x1������`D<;��}���>86[�m�ꍔ��=4��K<��`���?��OAB�z0��;'8�c�a}�+h��f�������۷��ʂ:tk�ig`���a�P6m�����IOw`] �pD�_�U�$#ͣ���}(z���� ��&c.�g{�O��O% �9d���/�b=������4K�w���7A�Q�Qg|~�PS�8p����Ƅ�B�C�����?��!��to�h�i�Qm������p��a�;L��B�O
���b�ǡe�+$y~7�:���~�*8ǅ��B�~�a���X���D{�2�
�00$b*��h#q�#�ejr�5�N v�N�r����@A�N�'��aG}|������C�4�� #����Rĩf���^z!���q�{l��Z��W��3��2��Ƌ��ϲ�����#ݳ�d	h� ��<�ij�ۈ��JH��:v�)Hm$���
��J�,2j\����'���p1�F���̦רG�HMqS�s��l�����Kz��X���!�i�!x��Bq��˧a�[�j��n�=���A��P�R��2�aj	5[@�f�qX������.������
O��ȗ� ���1���M����f�b]�C N��W�%���j�>��� (���D��;�j ��u�C��|����V����S��q#"��m+�����h�൷���z���?��+� �f�^&28����Qƕ 1�~<�|�q_��*Hz	Z���	�k91�� ��]���a��D���Cskf[���6.�F�sz	�Yk�������8%��"�|"dʇ<�2�:Q_H��v��`͆���D�.���/�-H.���@w6r�4 3(H8���P�	Hh"6�/�`�б�C�9�:j,Ѷο�w=�;Nf8S�j��\`$)!���g�4�y�)����vKa�e77��<�h�6�&�:Zqz��e4s�]�^�#��Y7�Ήb���)F7g.a�d�T~6��.���2J��ޮV��="�'	��l��%'6�����w�5���]F-6����&ph!����9ξ?�����	}%�
.�Q������
I��������+8L�������dw)�@ծ�C a�V�׹�&��L�r���
$���yp��KO�]����l��2���|�s�r��+x�z��G�h�`u��Gk��`_�GI1\����'!�ad�-r��s�k`b"�g̫�V>�2�5&���&�F��X/ly�*��\
��3G�yh�NzE��3_�>8)ΠY9�Ϝ��b�e+�Q��g��f��?��I��L�#�8�CM�i��;/�7>��x����e&�S��#�ر���}4u������o�I�m�d���6�EzH��Es�h3�O�텿�����Ú�'T��6Շh��|��?C�_�x���;m��h��u�b�(�
Oޡn��O����
Sm�����c�u�,�	l^ۏ��WX����Bx�[4����6���R��v�U3U4	"��h�s��h[]ѭn�>h��fIfG��``Abj�Fd�e}r!����<�9�Q@��TPe�FA%�v��I�Ge�c"�B�PNJ�&'��QۼF��S����`^��	���!ռ'��
�O�}HO���AvU�MjLf��+�t�
����G�;��-P�B��C(�B�M3>�	P���k�Ͼ�>S�Iu�dS����jc\�f�"o����w��ԑ�9�Gň?�ePW��[pǔ�!��ݟ����c.������j���D��|���!O����Lye��Y�����s��������S�bLa�	Scn���`�Iv+VC�EҠ��1>	s�-A�b�yJ�[���$��� ��^w��L��@x������L�"$#��0�!����S��A*mg�w؀��QW��X.���S0/�9R����lR*P��2��T^^F��I�e,ZQ	pٳ�u̵@a�tz��*gP߽��T]����=�jïH{��E-s���I��{��c��dhD�:O�ƺz�n�?a_��g%�	7�0�]E]�@8E�߽�h�m�@��i>�;KD�I,]c������ ��`�i����{��s��lĻ�CS�"���e[Su���-F��_R�;��+,��sb8Ђ�g�#� E�:�?T��|�� �Yd/�L|��ig����#����\V���l�!�2�zWZ�L-@m�?�X�Tg��a����&t.h)� �y�N��9^"��:��͙�k8r�	��l�.�� 4   #�;�{
�0F=���reY^.t\ta�_��U�ZT���vJ������/�p�         �  x��W�rG}n��>��u�<bkb�a�>�K]������-���=53�"�k� �A���:y�����X��.��"x��p��8��M�y?�,����rZ��t��Ӵ�%��<�iY�;io��fX���$'���ר7�4e���d[>R5C��������%=fWiYn�}}��������9�������5�J�,���7�F��c󒓡\U��J~<���橎=��c���jx�}�:�J��r����!t����ӿ��=Ύ$��AOJ`�D�J��M����T2�B�X'�P�oY��>M���M/���������z_��̈́��vx�a�U�-��-�Y��\Ȫehd���1�a�OR��/�7��<�YY�l5�aOg���|���y��5��< %�8�=E _K����̽���VN���(Ք1{^[E��D{�+�u�h���
@�8��2���r@+��μ#��}�l^���z�������q:�j+�7�soL��iJI�;|a�V������|��,;�	�L�����3~���o�|��YF���:Q6��m�D� ,V^Zi���msCٍ@g~���c����O!�ߣ�EKQqJUrD�AF���F�2�q���u�w��8OK���5]��R���������[o��~~��a)���>f7�c9���-�ٍ��Ф���n�nNw?�(e+�$2���Ni���g�%���m�d:Bdz�N�C-����Bc��i��݇#��n�꺎oi٨X�6�'�f��M��2%E5F��3�xG����OOg;�ߜ�M�|�Ԟ:@�r9Aq�v��3�L�����\1p~R���!mY4�pƵLe���Q����)_���c���|u���zn�����Q���!�����'��!ي���M��-D,Κ:��'p�	�,�B��=E}M��cn����T�k=�����LF�5��������{���
'� ?�
:��"�Բ�M�L�1*WÛ����=��{����=���S§�l�SǺD����SkN�l��m�s�8��s����q��E�����,��qZ"�^>{��
�#�1������ŨSbU�m8_aw�y��h:����&��>�{�C��@`b?��Z���M9���~=�CD�C�fX��!(�A�
D�I*ý=�˽�SmMC��
�I��.3yTAAw��Z$]?�j���kx�B�㚣 �����_��Uʹl�����6���HQ���d�'�-�[��2�oh87��� e�,�
`�	ܯܫ����,�/����˖u�1rY���'X\�N��o��ƶs$h���؉;�o�j��M�ѷ?s��'�������Ƿ���p�[O�*h���2��#|�?3.eS�Z4h��'=�AP��F��o�?7������/�m]���,SI���/�YzL���m�%փ���%n
M�PU6*+�l�>�}#*4��=�[4��'X\L���������fME��j�y��h㮟�h�R���ɵ��E��6IrI	&�PT\IUBjU����<B %,��){�z��B�Tjڐ�S�|eG��Ku��x\U�Na*��n�
j��70���䨃���V&�h��$���)
��~_��bL�*�Q�}��x��qWdjZ�(��Bk��yYK����6��%�J(���,._Ýn&�j�FLE����ĄQ����i���u37������&
G�
�*��>��q?5�K=�f����ѣG�*��8         �   x�}��m�0��5�`!���z�$�0�؉l7�Ƿ�ӻ}�XI&O�%F�F	Tr����%֡��l����6�l,�����n�%��ȣ�@BD���5�t���c���9���J���j >EH3��E���T����]�b�T�")Ԣ�z�Sc@I�T����I��>?�so��Vy         �   x���Ar� @ѵ}1�w�F��0����e�4x�[��� &	�.Z��#���y{�1�}n�W�)��[���v�R���jg���V���@�H4��$�ƭ�5d����}I<�O�Y��\�t��cv��-��u��Z�Gȑ���QB�v(�0��(k�e�D&��|>���}�h?h�      -      x������ � �      �   �  x��WMO\G</���O�:���1�)�(J�b)���vV��r�,���{���a��;=]5�]5��r�.��z�ԃ��s~X��������5]+�_l��6�\�P&��2%��3�Tj5�z�y�B�c���|����u?���Vw{�Q��5��_��I�RZ+�2[��Q�����2;�Ӻ�E.ow�5I����Œ�Rꅺ�Qo���f/)n�VF7g<g��3�?�}y���$���T�G
o>P�q���Ǜ|���{��߮�?>���� *6Jg�]��[:�E~�i�~sy�Ⱦ��<�ZJ�׺�,4b`�GL�yW�*�����b39�*�9G����gϢ������{`Y����8:��eKF��FV�%5�Z����Y�
�I�i͓kv$m9���F��5Q�8U_�����z���x^lw��Mg�T�F9V��Z�9<fF�ui�:ݜ�qw�9���4�C�T��[=S��r�B�2�PR���ƙ�ai����6<d9߮$&����L�9&v3�P�IC��k�t��3�Z���]��DLx�.f���DS}���������wk.Tr�	�K�(�zW��!�Ca�@�������c��W?�~��Sd��ޝ��)���~K�U��τ�x����:~X�者�37sPF(DjN�[	h$Hr�!���=���$s��Si	��Q4��R m��i�����,f�q���X�0�*T�UU-����W��s�wz�G��b �D;�T1Pg5�l���1>@�ɢ<j�ǹ^0���"��2���<q�A:|q�D�O\����\)���\U�fMZ���1~)�/��|	�'r�5�)�E����ĥZ�}�k�ahrL���4�]��ǒZ�=����:�s#��N�rHĆ��Bh��B���Ĉ΃"�J�^s�Хkk��%��UJl�� {��X��[߼��e:���X�P��3��+�D�f�l�~ܙ�?5z�;/��u�'���|\,�?=L�I&��j�(T����/'?9鋿B+�4�*xg�\�/(�k�'51?pC����	�'/�WY�4\�[X�o�噯�k�^}�P!-���{�e
����i�K���A���ׂ�㪜c�}�*\1,�(�a�?ж�����pĹ���e��w�b�}��7��+��% s��g� @
�W�ri�"�gb�V���a��7�?_��oONN�n��      �     x������0@���$"�@����T9�Ӣ�P�0�����P�ݎ�{A����3���U�[a��o���>G�~զ=\ߑ$ξ<.0��$'ʗ��3:S	��]������"���FuO!ޚ��{-��Z���4X����v�:W=���i��ٮ�
�Z%ZzTV�#��M=� ���B7U-J%�@�&Ԟ�n�!���`e����s��.�2@����,�3�a�
.s|!!�� E\������/1e�4���s�<L�����[��S��S��N��N�3��10���2��؄[�:�,�P��}⸖�N��m��ʰ�ӭ�r�t�cbu6��{2p�0�u����*��w����!]G�*��z�ns�Q<j�A��Q2b�@�!:�և�V������<���<�2<��=d'�!7|�C&d�Cn&�r	;2?���dxȠ��C�u^(Q��QE%�lS�:�@�;�t�Ǿq	;}�"����=,�Ww�����1�w�{ˇ�� 0`����c��?�v�<��X�r      8      x������ � �      �   �   x����q0�Ͻ���%c���t��J���6==Af4�,ǉz�[X>�U�`���ak',G�)H�2��:2P�S x�c��F[X�C7ٽ��� �&8z*��S����5
ʒ@��+�Q�ʬ���3�������NӁm�x�&�ߟ1�C�K�      7      x������ � �            x������ � �      !     x���ˎGE��W�0����d� F	�M �UŊ;��$����HG���N]����J��ؐ!P���;�F��,�PB����i��#�*���Og9�~9�����e�����8Ng��T��|?�=ҳylP�o�k���H��
�9���(!`qmw\&�8���M��w~���rz�i�h�EGKK�E�t^#u�;Y�~w8.}�dc���9�qN2�1ѯJC�,ĭ����Nx�6�<߽SI	[ [@#�v&v��	��&�@]�qk����ݾSr�Y�@���M�L�'
���çe�ܷ�����ιrccL% Q"�:�b�F�2鷪��r\�Or�X�4����I�g����2ߋ�Hգ�@��j��m�Ru�&�')��?�y[
<M˓�A��w�rI>����j�>T�K�`�͡$�=j��z\�����>;���?���]��ռ��n��s�lr��7�mm�=������*��v�LJ��)!�����/�~Q}okS��*�-����w@����b���7�ᦴ��z�1(�lpT�0�^}wW�7K�
9a��\��c�Z9t�(���g�7K�Źz+��u������x�c�e��_9����P�"�T
tOu�4��X"�L�ԯ��)�-��(��2���[�����nT˿hߘ������*�4�!Gݴmo�59������y\S�DԴ�QYiր���u���x��1^��]�V��c=�ˬ5�y���:�:�2�/�-]�֟�h7^b�a�������?�<��      "   �  x���ͮ�8��7ϑ-��~�,f;���n(���&q
���e�n���b�����<G�PS'���yP;{��E;���_n�/�ҏ��I�.�C��=�K���w:���/c��g}ך.ˇvyտ~r>������B7��k�E7�W �Jv�A�Ic��}K�c_������ۜ8!<!C�=5Rcy���=��%n����^/�x��[���N_�����_{~y�`��!jp.� SLP��W�����W�a���Y���Y�V�b+g�}��m� N�!�w#S¼ek���K���!�׵bc��B	(@�ga�l/�T�L[Ʀ6��\Γc8�jm�ֶI�S�5sf���T������~�ş���Ӳ�$n�����4@�o�Csn�|��nl
�[O�~�@��n4g���� .�kv%�N���5cA?<tB/PJ�������xL1���j��ZP�/@��Z'�R#9��YS�?i.�����W�n���+���MqS�'�k^��L��G���B�2�,}�k nNE�ha���)�S�5�j�So�1C�� �I	�sra�� �-[S�'�k^�Zsٞ�ԇu]@���kx����n���V�M��vb��mځsD�&��r<6�i��TsM����2��Ѥ�G\
A����گJ�V7�
��L��ڑBB�$�ZF-Q;(�N'����s��i`
�fJE�V8��D�b�{�S�Rg���b
�O��n�C�d�      9   >  x���ˍc9 �ݹp���\�B����갗��|TQ&�^�Xk�t{EZ�'��&�J��k���{z���|���eʹŋ�q{o^���mMO�t�'�O��Ҷ���ǫFY'��R��׃�[�}����%ʲ�N��2ޭ����A#�l��������s�r��4-G�d��ճ�5�:e�ُ͞d��U,K<��Nk�{ҬyRV'�h-�܊��3����<�X/קY_����]&)����h�C�U;�{��1��n��]�wQ�*u��p�(+�ߵ�,j3�zb+o�w��e�ye�T�8ͤ�	V��h����v��?���l�kv���f��DG�2JN�tm�L�*���ÔU���y��&I-�us��(k�Y��'���������f^�gZ��`�چ~�s���X�/Ă~!���_����B~���ž;�_���B,�bA���X�/�/���_��`~1���_��B,�bA�`��_�/�~����X�/Ă~!���_����B~�Y����c1���X�/Ă~!�������Z�<��b´5�������MgQĂ�������\E,�*bAW��X�U�/�j_y�b�}5���,ַ��<��;������2��bQ,W�|�彥������m�ʊe�|=@�L�٘o�'�j�i��*�y��N����>գB�!)+%/;������ᢽo���uP�}���wc�"�Vo�{�k~)��7{gȩ��g��}d���!��LYhs�1b��%{<��qN���[�';����Шmk=\ma��h�t�k���
e�{m��D)=6�Ɨ��E��aQ��飬�j���v�c�d�X��ũ�vt;�+&����>	���$4�Rj�+�ZC��>Z��(=i/Ү�9:R}Q��-�*Q�s����8c�o������t���)k[�:l��p��-U�,1�-��-�a�o~$��B,�bA�����bq1���X�/Ă~����X�/Ă~��4�����X�/Ă~!�� �/Ă~!��9�/���_��B,���_��B,��}�_,.�bA���X�/4+@���X�/61�X\�/Ă~!���_�����G���VS��d�o��ͭ>:�"t����]e�b������]E,�*b�Ya��Tꍍi_n��+oڍH��U�=aѹ����AXt�@��s���E�¢sa����^N��$��x�6¯���3�1�.�>	��=��^U��l�����l�W���5�X���b�b�b5��b5�X���b��s��-�&8��Y�~�s�w���/Ƃw�E��	�ޓ��G,v� �'',zO�r ,���������f�      #   �   x�����1�3��c��>`p�!�7����.u#��L/�8���n].=����'��� mb�.&����ӘA	3HP���!uz��w�صd�A�:�͹������iwѾ��KWEjz�eo���钎1D�v����n�:���]^��"Cv����)�����8d�+˕��%���}��cK      �      x������ � �            x������ � �             x������ � �            x������ � �      �      x������ � �            x������ � �      5   �  x��VێG}���
���hٲ%�,Ǐ~����238޿OY�,H�JAZ!!��:s�v���!(�.�)�
6dO�9�U�>�q����q�F�3̦���~�[~�Z��u9��#���x3|׍ӀS�o��~������ɧ'O><z�M�yX��q6�-@.�e�`�`sQ�:�}����f�O�׮�>����G��pfg�+ރ���l���1`|��
Ssy�~�Z-��~ˋw݈eu�:�7�gCM6>��YI'�R6�-]f����r\�ޭ�������-s퐚�9C�����1[P�+4XUu�f����j�8�SO�j��[����:�x������-����퀴W�8x!�Ehy������X
��h`��v��t����\NS��%U��^��^t$��9���B(IX��BVMA����`�z5���-�|
F�y�,�&�"��<RJ$�8�w��a��Ϲ������d�[W}{��叹��8����ު(U�k�d���X�av�F������Y������V�,}]�h����@e�<^<7�l����	 �ps��Xx�N*9 e���_7�n��H=ݒ�ޗ [I�u>I�Ф�b��.c�*=g���EԞ�-�R�,�NU�7(�|Z��*�F���K>�jb˕�g��[$Bf����4����3�p�#���NØ��JI)�v$GN&��s�5�K�p�t;�pm�-:H�Y8*�!fS�RD�:��������P"�[��!�#��Y�N3� 9a��qruqi�	��J4���h�UR�kF/�Q$����՞���b�JvZKU@�d�)��Y�.`k
�hL��y}�]B��j�����S�O������؈��������V9�����D����(��"]MTN��2p��r>���B�o      4      x��zٲ�:����_�q��~�Hj�WJ6fm h%Q%~���V�tݬ��3u&�,-uD ���N�-|���~�P�2Gj�:!��o-��/*�E��;"�(���Od��L���/y:e�I��={d:;9gy����x_M����ɹg'����o���*SN<GX�:�1
э縞�qc���#z�O�S��~�Rsr��q߫�a������¶�O��oR�@\OR��o���<���{���ӰnJ��I�i(����|�	1K�mi�4��<�{l̘�J��>v����s�|�Z�~[�|']Ɲ����}n��P���'aqbWȃH&�#�J"{V�o��' ?���
Tx��B�I�20�Q��X[W3!�7�_�h�����A��8 Q�q��ǚ����a��ɹ��E�����c�t%�A��kn<Nт}i⚔Y� i �O-4��:��L#J����]���K��l���B���c�]�í0"(~��C������p������3���CaH0���ߋ�O{sy8�}���zL!;�R:Ҥ�c�d4���"<���a�@-���8.0���
���|9Dʿ\�~H�	�4����R�	��������/K��"��v��}�c���6�+�l )�3Sc�b�<�my�� �R��Ͱ�b��˜���\�\�;��E!�S��XM���<_�`�t�4��o;��-�I�5N��T% :E��<_�
q��G�g>ӜK�=X�`PH}�MM@# �}P�����#�s�A�Q�sGB�:�4@b�Y������p�(MK?ꂘ�pGePj�Ғ���/* �R;'�|�F�]��tUK����65q��2,%@!)@`� �	E���EV' V�}��d�T��P�(�#	�@��6�`�� ".���b|�@`�9�äc�H��*�O�� YG@�u)��R�+�I
Sv�Ŭ��/L�J	He]��<K��Bh`��}���3�A�gA��0���..b3}Ł=��~�%��NF�=���t���H
���r���W�_�{�i�G��uW�'ZԢJ�zH�a��]����j�u��^�;ڌ�Ks��O��b�w���Ɋd������Z����(�Q�H�YA����6���h�V������[���G��>��3�~s�Ѱݫ�i�m�d^����������VF�Y���0��ř=����d��&驍k<����e/�l�����y��3Q���[>nƵA������l�^���f�'�(�� �sn���u��eO���:*�IԹ�W���Iq#r��.�ۻ2��/�����N��p3:ݢ��V�Mk��Q�o�c}��9�5&�d���)��m���;��I����y��m1���'y'/��uj*��%{��:]��<���6������ݐ�v<��{�^\ߵ��6^l���p;�ø�񻧊�<_�B���fkӽ̳�ˋV��7�u�Q��|�F{o���m���,������mѢ`��klb��=�k:�3��$}���`������o��y/Y^�(/�p���ڨ��e[���Fuo�>���Ö�����qQ����;�^�۞wЬyՆ�N�7o�饳�2>N/�5�̽Ŋd�8��yﮚ�����ˏZU�m�D۲7|�AoeptȬZ4��~ѓ����_������n�{2���[���w��i��z�T^F��A��^To�/\nw#�/�c>���Y�}�zm�7nD�ۣ�axu�-�z�ѯ�Cv��Uzش�wC�W���]޼�z?�aaw�b�8�(��3k�*������g��܍w�c��PNZ�i�E��8__o|���&a�o�jg�4ʼ�T���I�w��)�'�dC[�įfc82hH�B$��}�Ū�,��6l�n9�/����U���Ƒ��V��_�����>��z�U+PcZ��5�����!ne�G���Mn��m���֎'�I�՟�m��A�^��^���R9�{����j=721�@Jgs4��o��jĶ��T���4R,?̟�;�f�8tP��d�y��y3��ρ��F�~��*��s�o�H��痕��Z��������� h�����d;��M�=�hpԤ�؆oR��l��>Y7Ůj�;=�f���ެ����6�to&�A�D>]��-���$f���[�3k3��7�8��F�_�E�Ƀ}p��ɪ.���3�����m��=Y����Yu�i6rC���������z�^�Zt���e��$,�X
�ƴG4XVA�Rjĥ"����y��{��3��s��z6�M<������mwܷ�5�I��`:��2����dE��N�h�t��N��5�bwm�0Z�V��U��k�����kl��~�V8��fE�IqA@;��>�����I�8y���Q[<��z�a��h:�'�����9�����4���N��C��wzyhp�Z��t5|̇�!����}�\%����v&���t�{Q���N�
6���%g�=������TT�y��,�U9�e� ,icJz{o;<��)e��	o����*�հ{�:�������3�l�ê6�,`_��ݕM[au�U����"������<h���Q�"�t���bUT��O���	�uڍX��Z�ut���J��*-B2��L,洌�j�W���F�~�y�k���?����Mg��C�%��hv�h�C�|���c{
����"c��p�s��F\MY8�f�������p�Χ�S��fw�����8l��^"���ۇB9I����l�M��ʁP�l�8=�I8N���u{�n	��{t�O͸�{q�6����z���ζ�;��]X.��$�Q���MyX"�:X��q]N�������x���'�B_��L�1�i�zg�^���P�2���I�a���Cv]�]��Һ���U>��䜌��{z�n� ��}�D霩[Ң�~#�����"��͵������i_s~� :� ��k�(���\�����}�xa����2��?�2h*%4�q���"�W� p��!
�?�݌#�T8ă���|���l����@1\��w��������]����ln��<��N��������$��a��d�;�3 橦ؑ�#F�Ze1uY�{q����F����� a�σ>����xL	�D򃐔�A�a5���+	�+M�I���D�L#�3�2����#u�pKL����Gn
� ��TA����-7�V������4�>8	�����II�0�
d�rI�{!��H�dD�9,Հ����;)�8.W�����[�����uZ�g�}��GҼ��<�y3j�w!�su�"���UnX��8�͚zi⶚V�":���^7
~((�S�q�<���@��_Ҿ1�����<�i��������}�S_8�ꏩ4��i�A5سq��/�&B�T�1	��)����K��<X��nV�V�SG��YĦDr�bX㷗�#X�o#��k�;KP�>L��Yk������C���;k��9Q���p5,�˻����f/ݠ��G[ѱ3/��l����1�����q��7Z�������{oU��o����j�EE|�zŏy��u�-����h2��9Y]����Ū�G�s��rA�EV{�/O&��0��q�[R���� �>h�}��'��;��]���f~�����"���(�n�ⱸ����"��v����[�پ�i��O�}�u��f4��ic�v�\��`0�c�2g�y+����������֬�]�Q�yYĨo�L�t7,�h��a_ؐ��|����Ѱ��IT��p~���a�g�[ޛy#��S�=��/Զ��n�;O����X{���8����{߉]��=Q:�3�:�Z�ҟZ1O&����O�S1l��!ٽ�?N�k{W�Z��8*F�Z��=�s��Z�^��C�v�����4.���Ұ������;Ae���7�u�ѝk5��2t���+25��т��N���^�+�ݦ;S���D�w��^z`�bD�c'�[�&�8����q�i�a���{�y�٩������r�ǝ���gASn�`HI���w��ݦɊ�| �	  ��&P-R�n�[�z4���cO�j�>�����ካ��y/Iu^��Ov���a���2qo֓�d�:��m�{��p��g�E�˺�w����
?��C�hO7b�����RDɠ����A`
��e��h`רh4ta�ǭ�[�q�,�`$�����d3|��#F�ƭ���J�e��^Z�Y�)�O�{�Y}6�w�sz�%��z��2����\��<�W��U����\7r�7Ne[<�n��PR䝔���:�*/�cO�}��);�]�zMD�p���>ЯZU�Ԡ����R���i���,N�,/�I���2R[�u��)�ؚ��u�n���?bב{:=��>���Z'�G�EQӋ��\Ӹ�(���v:*��P�n�$�6x��7���^�:&1��A�g[rW���wY6�=�_���"Ϊm�2M^�UbtZ�p����g�����D��`W%�n���&��y�b����6������^��3���?���6���d�F0:+�D��z�&��nt�| ����E���1�X�w��r��������ˠd}7����j��3���1[���]�:K2^��t�aՉ�i5KhF�B�|�����v�M�8݇�ݾ���~����ݥ��r�����"BH9F�ϐ1�����"�����{���f0����3�1���\��2�n�N]ub}nkw�!�O�_a�)���:���P��M�Nƽax��u�Z�g�z�Ԏ�M����]��kv��9	����5Q[�Ͳ���E���e�l���9(f���r2�b9��ћ���C��]�f���j�F����>g�����+#yn_�^"����ϛT��1����t����:7��+��fn0�w�YE �Ǎ��w4�V]P������0��MԮ}tW�)��}�l��̾�I�}[?��-`�:��Z��>ݚ�N?�ڣ���H���m�{�R�ޯqY��`s�U��`�F��������)l�}X�ww���"L�l�/�*H�V#�"1����"�q���gSFi�M�rR5���-�A�g�2^9F�K�[.D4Tӭ_���A���M��b��ш���Зx�-�*-v���j�Xc�Z�3Ɉ�up�E8vލ��S��?��Y������(ϋ��Te�4j5ڏ��~��Q�1p��]�ѱ���s���吝�Q��;�^�����8X��������d���m�+�$�mݍ�m���j2�kTy��v��b|y���4:�����=>�j�̟N������L!��Q����j=����g���ϋ������7lںT���ҟ^Ă7��I
��~Z(��	�[PЄ�F�|�$_!�!�������F�u�����B��d���y�h}k>7L`Y�q�{�&�����i��3�������-��K�/����@�W�
	Gj�:JK.`3k�ŘW�b�P��OcNL8�pj<�)���iZ=XNvSg�i<�ҩ�����G��_�6�7H�Z��0�{�v�߬����5V.V�2���Ww:�QlNW�T� B�
� �!�(��6�J�"������� ����ݡ�r���9.8���<�{+0C)K�y��S�@�.�AjN<������.��\��L1*Ġ�$��C��B�f��r���7t)TUH��}
�*����`���讟�� ��X׺RSm\��rK#(��?�\�P���F�`f������]�:�s)�~�r�9>K����߼����S����f��	NSm�-�_n)H�Qa�wl��V~�i��	ܤ��G�q��CH��E�G�hp���ɴ�<�S�m{���p����fDA��th'Q�H�T����hoa}�&�D�'c��� l�z߶����@\��8�}.��
�{�q��v]�{�"�`@�����0�?S Wɐ�kj�7��_\T�R�TX�@@�t����28%���Z�%=��~�&`� +!Y��Z$=��Wq��CJ⼟���l�w������/ו>��R��s|�Rǵ�9	�@��p���$%Z"��0(�8 �|hJ��)�<���u�Ly<u�@���k`�p�`�}1"8%PM�?s^1�_����¸p�*���Q*��0]���'V��>���P}��*�Ɔ���y������v1�����A�w}@�Y|����s��%�ԓ>60n��<�9Ճ��z�1��JRe�ڇ��9���N�OT� ?�S��c
m-��*ς ������?�z!�P����~��"� 2u$Hq�V1-�m��ynxJ�������G��K �	h�X7����ʬ�}�k�yǁ~��@z�q�蒯ك��\4�����0�R��V��ӿ`���?~��� ���"      �     x��X�q9{{r�(�sُDJ����@�)�Ճ�����чvjيDX)|_��\f.[��,�W�I5I�/��"���̞w�o� �������$�{h�a�>���y�!u_c�9Ȓ��C�'i��z�^�IWi��H���.���N�ζ��\�imRCvօ��� �yW�����s���ɧP��ɍ%XNp�7$k9zW�U��Nf45�R�-�ir���v��T<��k�����>d��Ǌ�-È�:��*�ᯧ��͏1��s-�������[�r��cLڏ.���G�C�x�]�{���7$�}�Ic��2h���RUC��<2s�`�P��=�����P��V�W>�8wpvڵѻŃb�C�褅.��:͒j��nm��uR$>j��zt�NR�)��AAL�
�t!�2�������>0h:͂�T3B�AM+�x:�l�����]߃�ͣ�l%�{��:=!=��"=oxQ��!����;��6�I��K��bF�2�9���'S�v�����\a�m���U<��|�<<6ގi���(��dn��;��Fйfq-�e�`�&�Ǩ��3+ڣ����77��%�9gR�ա�=XoH��B:���;z�d�;������~d2�<��E
L��?��ef{C�6�:&��"f���榖%�q��	�Zg�Z��n�9!��rS�[oH�����K'4������cL}
��N�BN���7��E��ˏ���$<��8�O1���*g�Wf:WcE�D7bځm���l����<"eam�v�n�(� �<;�M��<��ʄf
�C�"VaZ+��AF��$�@�oߍ���7�؛�z罯u�H�^��|l�>5	�
�u~�R�s�"J�	}�g�i�@�����-���v��m���6�_Lz��r��c� j�ȲO�P�y�Z.��p���l����.�u3��k�k�^�NOHO1�H�[���a;i��pvM�C[T�C��d˟��b��
	j�a���,��׾m�7���zlXϰ���C�� �M�� �7$<��G?�CJ��||�H^8��IKx�čp��pџ\XC�w{�cvf�F	�x>҉��i`�Vn^3��n۱�?���4@�\߾SXߘ������UH�8>�ǬS���yCʘwڮ��m��#>'7�5k�7$.e�D�~�Uw���:�ll���O��� �4��_���!��T��U���os��`#��izc=� ur�
�&�勊�`�}�����Et��m�m>�jL
zSM�����8C����U���M63$�<�qZ*K�}>fVl���xX����*	_;@*8��l����ۈZu�ϩG���;T�ȥ��>��j�
��B~/��V�'���^������;�S����9�љ���+Q���:Ђn�'�4� ���qX�HSE����I��F��?�iҰm�&�0Q�,������1���d�_S$� $�K�P������w?N��hǭ����E#;��rp��xK�߿��~�� ���      �   �  x���Is�8�s�3��5־tU ��%K��h��Ît��k�'l�l=�~�$c�9ȴ������⠒�;L-��9���<��.�&����1�Z��1�d&Ti�u�8ĒH��������I�߳��:e��f�Dxv��j?�ˤ=[̨h����NN�<]a��(.E��(�JT��#�ȷפǫK��?Y���F��?j�j�^W��r^<�O�_���2z�y��w���.��*;��m��w,��jz��E��|�7+�1(�3!x�-�A0cP�P��,��i�17J0���-��x@��@�@E1\Z�3��U�!L�ұ�9L�J�A�;Ofe3[6ڗ�>y����B>?6�u5-�y��"��Ak�oq�Y��秨����471O�2�S	�����a�&[�o����5%�5�TR  � u��)�;c5C�Sq�Č�r$���"�G`C��x.���
q�8�J�D�M܍��#6��6n7�ˬ=ڶ��������E7���:�Ã��ii���$�	ި����aT��w��-���O�<�
:YD�3�Y}�6�'��%%J��2 %	�@�|^چ�T
��F	A�e"(���0�VM�{+B�$����^B�o��qzek�0do�ש7P�I��azڅgoW��������څx����1��:����ٸ��O�oT9����i���բ5�ۋ���ø�����
rc&��R�,i
���Ho{�o�
	"�I@u&�a =A1g�o%ι�P��*�1ɓ�ͯ�Z���lM���_��]�k�t��>ͷ��i~�t�媃�mt��~i�����t�Y�ms�����[&�Ǫ׾�i��gX�_�%	�L�a�&�<�NYn*0��F�gZ9� �^%%�������"�৒�B(B�n����`��Y4�6ʼw����L6.�8�:?L�)�<�4��٪��L��RK{��XdIk��#�G"���J�j~�T��QgW%��@����_��_���� `^
�      �      x��]�n[G��}�~ SS��`��)��X"�\�h@��f["Ք���s���"�FIdf��,�-�S�پS�*��?'��|���;u?v8�����\}�����bn���8�O�����?�ym:�Y|����4���W�Au0�o����	?�c�\���f��q!dA"�I$��Hq�`E%#�IM�,�*��05�2��M��������A�;�������tz1��'?�)�w:����^L��	��n���*����G���_?j�?��ӽGGGE�aA��K?����#*~����[�yn\)κ���psIW4h!��X!섒c���
�҂���@�z|�%A�����օ(F�*k�t�	,�@R3ψ�ES�pnתaTɀ٤?n	Kw:��7���;G�Jd@���c���~0��ך�{�˻��fl+��Fq8J��$(�l%�	|�O�%����>�iD��� ��X�o����?�����o�,/��'��=-�fdR���G���_�fZ�NT*ĥ*���N���
�*,3JQJ{�W�?F��ǣ�7:�8��[�荆'��U��Fe�Nk]�mwO �>�%s�#Q���/�jp�pa���khmI�q5> Т����H�������6*8��"|�&i�����ƥ_���ߏ����7wq��TYQ-F\+Զ)���K���� �P�S!"V�)�F5m�����].��z㩏��,H����P*�!,&D�Ր6Y�s*c��koJ��7�~P/)=B�x
^!�8�DI�27�����}H�%,x*�Zn"��G7�g����٠Q�a��`@ˀ:8��T�Hu�\ �D�x�Xňۜ�wg��iw:�4/��_
L����ɨ�����N�Cm$�F�t0�Pߜ��ņf4��eB8c�.bb�R�`8�$b��eL��(���q��¼��^� ol���j��Ҕ8c�2P�TD�I΃J�"� ��cƑ.vx�Z�q�{z�G��=����(R_0������6��a�.����Y}���q���������a*;���<|Qz���#������UxE� (�m���B�C�Q팕���8�.�
���r��z�����D`p�9���$��r%N1	V1��X��R���uk�7B�c��Q���wZ3w����E�{�e���6����H�x�36���y��.b4id6���W׷���R|dM�
I
ꣶ�Qc�gHi����t���ZWQ �`�ǣ�y��Y�vr�X2V 6#k�U�֨oƞ����Ǔ��o_;����������>�_ �5��`RU�PR���9�}*u{�p��єr@' 	@E�J�N�(���i����[��;�Z���Nz��-���.o�w���G?.��{�U��m߰W�2�4҂d�"">�:*� �&Ұ(���((cAi��}�q����������uOߏƃ釳m�`����u��/���5�qgn�w+:7a�[Xu���ݹ6�[\|�4�-��o�����New&�X*yQF3U��".S.�"�	g
�E�0b��)�d4�@u��O`���A��r�����v�׺�B/#e�*%#'x;|G���8a��ZP�4�!K��}���4��Qa��\��h��Hst��%�S���O��Qj��6�q����L�W�i����w����nn�W��R&�nTG�ds��5)$��DA2�	��{ޓ{o���EB�*q�D|�~��[3�~����x�)Y(�jo��yn����������$5E���{�ҕ�q��I�hA2J���w�c���VӨS�"-\���%��&i�Q�k �ԇ�m���h)�o�)*l��0J:v��>��f��}b~�p��Ywp�-�����*�N���(��3�'�SUQ���z���$?i�o�z�#�ᠧS��7T@�1��+L@r�3��jC��j��tN���ۆ�f����Bų�f/�35��)�5���w3�~��]Q;w+Y6�\K���=[Հ��ب�.$/�	�kg�k����H㐷�|I0)�g�$"��f��5�_?\)���aH?3���s�W#
!_��"�	K����n<�����<�oL���?�L
ZS��W�۶78�h7=3nP<�6h���!�/h�49�J9,�H��	t�Kĭp���-���v�Ts�_;x��W�O��Y�*��W?Y�����Ԗ��f��5_ۈ�A[Ϥv�y��TA�N�� �ƥA�s%$�6LS��=��`@��R����r��3okf�c#����2|��ը�#|�����I��֌IQ ������Ģ��s�}���D#F���vJ_��m���?y��
4#Y+�2I�v&rA��6*� +E���G����ɩ��;����-�n�Cg�2�2_�.f5��
�kD@|����dP��#�{���*���&)?�Oz��yj��:m�&����>9�\�l��K���~�n�Fʐ��Voo^,Έ���xHF���i�I��L!c0�� �X5`���˹�p�	��ڹ���^�n��K�my5_���-`�gB&���;d���t�E	��h%�F^Uzm৏����y�����~M�SX0VЌ,p &��c�_h-I�Y�h�T&�w��_�g����XM���W��$�^g|��2:,V��P9�YČK�Ĳ��,�N�沭E�5,D�R5�����c�\�i�0��x�B`O�Eثe�� �t'�o,�К�[a���C�q��W $��r�������0����kkۛ�<?Ի��M��ALn%b����K�AN��X�Xj�`�D�F3E��3��#R�����mJ�eu�-ɒXk�E_'����uE��\;\�1^�i��f"<z�g��	�`Y6+�E�+n���Y)#��H)�������ñ�H,Ke�J*�h!�g�@�{�b^)D1��:�y�s��i)'6;}k����޲!=�k�ڌG5=<'EX�_�Y�1J�X� �6��y	��AP���{��f�T�H�C҄��Z�ChY��ܻ`R$���\zju��`��r������C ���l��+aZEeq��SJ��}�W�!�]�)�}�K���vB�/�����v�&�l,���q+�Y@v���w{}PAҏ,�%!�#�bq��Z	�+� �E�n�~�S��^�b���g=ϝ���"C7�_:�˯aѹ2��o�n��!%�!����IU(���(!���8��?�ǽ�/�"\T��dA
��4Q)+�eh}�����<0���u[���n}~o�s��|۹���Y�s̽MU��+p�U�7�viE�ķz��.x^u�F�"����q�h���{�k6qOfO%��e�qo�b�{�Y:���M���K&y��IZ�t���zx5.� ����s΅p�ҙ�,D$pGG-�֫���=�݋�DknX���HH.
�e�@1h�j�ྼD�:��Q�Gn�����]��KR\}5��MKPjD�i��׌0%�Qͣ��[0��p$P�PU� �dH�e�R޾��jk)J�����w�+���q�tE�5��}�J'�Re���`x<�ot�)�>=Q��{S6/W����;�!�qg�}w��U�1@>K��"�?Z-�"AE���TNpa�:/�X����ir�J�^K8�e�b"��:E9��v� u����XA%_�֢i�>�~�}:�b4����>�ޟ�Y���to���!?��3���xn�x��7Ā�8�����IY��:BRh�U��ϔ��
�B�(�uq�W�p�����}��:'S�a=r=l��Oo6��l�~��ͽ�Z�ò�y���K�t�̚T"�ХUK�f�!�>�����J�0!�\��Z��Ey����`�$0�%���k�K��g�A�czH+��ٴ�����Bgj�(S���d�aD0����y�q�^��^� ��],�yh��Ƌb�p!2�ٖU6F�Z� /  L7�H�V޺ �׫�.���@ j��ɯo��b Cs�����Z���O�P\}1�4mr��G��b$��D�^�1�-���yn�3�de�JMcw�l<���顲u�]�͸6K�T���/�5üM�������OQ,����V r�`n��m��V�3�5� ���EJD�8�L�Va��;g@3���9�qL�oƛ/�ղ���j�8@��b'�"�<C���u,��PBoOc������}6 �z��M���7nn�T��Lj���z� $���Bd4�0l��I���@i|�c4��1��H%r����k��igAd�Iz
�?������AyB=0s*����K���H�R'B��`�9)D޼O�:�#���"�-TH Uy��Y�U�im��s��?i�=U��/M	c�7��Lh��st8p��s�����Ɣj���c�%�wohI1wF`�)NM�*@
I"�
���E��}��ڜ.�����9��
�-4&��7���_¢��U����(��؝�K�r�S&�G�6Tޤ�X-ڑCBB�´j_^`�T!���$�	�jpH2��e���x:mUw[e�Hݷ��,���;ٺj���C�5
M�#���b���&�Ỹ�6���LKQ~��U���$2�����.��Xü�&�s��T�1MH��d2�8��rq:8�Oλ	��I����#Z�`�����ͤ�ٴv���N��	���<J֖'Hjc�U�r�NP��cn�s�c+�w]��ʕ_.�)�Sk�`���stˇ��R��B�4Z��Ii�h!�,58\���"� I�0*�%�o��9�P�a���F�u��#8��t�ۇ�.�N
�sM���� Zԉt]2�,� ɃՀ�������-Y��� ^e�!)�\2�jO�6�d��$r�4��(%6��l����o[�e0����g��e�v_�|�Jy�ƆzE���J��r���2^{P�7���ɚ�ը{�x�~0�<����x0:~Γ��
l"����F�[���7�� Q��%�7	Rȏ5�Ǆ�h<�L����X_�+�_-=�{~��һ7���P�z�.���7?���4�Ay�ƍ�����e��K��R�ө
��1�R�4-_��=ʍI:O4�����ۙ/�*�4;��8�]��^`��J�F�G��e6BNԕGY���T�;S|H��%�Svy�}�][h�)n@{ �����C'�]���N��R�$A`�"�$�X��\b'�����r����N��	6�8O�Uz:c��S=���a��?ړ��
�U�8���L��nl�PA
��U���Z+\�<������k��:�L#)*���v�f v�Oz�>�O�Ϛ�t8���:�tn��^�h�ys����-���B��w �%<:j���;FE� ��%-͂n�Ҷ�ndx����(Tvs���m�X��4B8���H�<�2���p`�t���-@S���@H*C�����V�M4F�����h˩j4���8�����m�X~�!����I��|�-��̌2��*?+TOSD��G��E+��V��՘�����Iζ��������MSg      �      x�3�L��".Cd�!����� �p�      :   �  x�U�Ar1D��߅�@�#�� ;{a;�S���lg�g�_�f�_��q�l0t#0Z�oEp�u����|�}�Cb�:x��AX�ť�r�`�����6ƎL
�{��]�Ğ����mDg�&�R(�uԶk��㥞��Pt�7ZI$A�'a�4���+7��<�au2`�	fڠq�ws��?Z��&�Px� թ�#���nm�$K4:7���[�y�
�'�gτ�Ѫ�:�g�6q�mu>������{���4a"l&64X+��7�}��fp��bt�����z-:�nT��2 �T� C�5��Z6���f{��w./>ƣhrq�}:h�c=�fPޱ�&����q%&�*�g� ^�T ��9=�eQ��JH�zӅy���96�uC���:A���}�ԼErŽ�������Q�V+�m��zW���Na��aISg�-\[R���z>�� IW�E      �      x������ � �      .      x������ � �      /      x������ � �      <      x������ � �      0      x������ � �      1      x������ � �      2      x������ � �      3      x������ � �            x������ � �      6      x������ � �            x������ � �            x������ � �            x������ � �            x������ � �            x������ � �            x������ � �            x������ � �      �   �
  x��Z�r�|��#�H���_�- $7�Ȓ#˛ڤ����Ͳe��ٗ�Ù93-�@w�S�>$O�\�9Q+:ɻ1��g1w�A>ݍۋy����\�_������������<~{�[.b�Ro�RL<���)��G5o��W���!w��y��ϋ����G/�(o��m0%${�6m\��)�K42���wj�qj�:����7����q}������Cw|��;_$Ȭ���lP�aT[Pniz��	�����|��,IOnFGqJ �p�|�&7|-�%���Ӹ�����.�8K��*��1��G�b�p�X��:� {��'h��]pe��d��R#5��J��8��]π����0؅���S"�~b��L5�He�\j��Ԅ^v�����>�������>���SS��9�8���6؇(������7�����˛�G�_�Ng-��Qż Y��ϵ+o� ���s�{9ڎb��D0өa♅�7����q�O��=����v�ip���Z3猦��Z}�ޟ���_�w2��eT
��&W��{)�w�#����j�/��(��M��k$kPZ�Ugoc�>��^^������1�'���dʈot-Il'��7�����	�cwie�(���J���UgIC�_v��?��?����`�{1��"��w���XU�R�����j�oX�%��S���C|u�	Im]ͽ�&���B���a��5�˕ص����5P�u$�t=����no>|�0����_9;�Kj��J���ѱ�x��Zs�$��C�#Đλ$nN$�7ԇ���W��GO��o��3�gY��tm�̛�.�����f7����z���&�:���%q"zR㠩�,��!�@����קoa>�[r�8JZ��!H�Q�V���3S-?^]�}�{�d/���(* B_��E\��Ș�p)��8Ӏ��G��s|-Q� Na#�r��2�[G��$��}=���@k%Q����T�/4��^���������ǃ�m&���r�U���Ϳh���'`��J�܅L2,r�+go֛ە��>��n�)���n��p`*�_A�FEr$��GǊc����?p3'2�>-�'������=H���?	�8=�d����\�6櫇|� �~#6^z�(�%eGˍ�Q5�V#��8*hH.�����`p@��P�`~�C\�M��#�q�Q�N@�KT8l�Փ�-�/}���v�Q`\F�%q�, �sD��tȽ;3sg���q���S�ʍq�=Q�;]���.��f;�(�`"(�Y�U�*�_���L̳��F;�(���4�C�A ���P�
A�1�Sq;ю8
��ȹ���D�u�ZZf#CҶޠ��TrnG��������g�Q�3<bn)�n�P�П�#��+�'�nL�(jK$��J�9V�$�7����[�yh�EK��t�d{��,x�O�G�>`[*U䈩E2��=Ai�ϰ� �َ8
/7(�	�W�$LL�[�����f����jG^�3���)J�[6�EX�9gH����`o�#���iIL�� sک�)�8�M�B��`֎8\!�(ZR��u�Bj&]qLC'���]�G��#T=DM�0H�:ۄ��>��9�����3f��`�Z´�!��Uc��d�]~ގ8,$l� ��	�'�\�B���|��bL�l�,5P��	�1��,c�<������z�|1�����Ϸr�v膫���� `����0W�;��u:�������
ds�`nV���0r�rq�����z����\i?HR�B~p3��`{-���EM�*��-ۄt��:�Zzޝ�7��>�)�s��ޏ��D�1L>����V������G�N��%�[�|��@&�ͥ�r������}u���,�����CWI�-�X���?���X�U)�q69�Ea�A&�c���j�������{����%Ci/�	�J�5�]�o���"��,�sB]l��k�Rj�Rf�z�fG��>�PV�J�iu�x9U���b	�3�&�5�M�}�
a��z5i5�dW����8�瓫pOu�#O�TT�H(�`nM�w�c���."��!?"�O��FH�P��w �R�֢�dim���	���S�k�#`�P�9��!;�=�w�S�~ �O�K���I�Wg"�K�TɅ֥����)��U�'������u�P�p(�:���f�"|���hIQ+rG�T���׋s�\��r�*g3�Q�AW_�q�)���$�����-w��Ä�-���3���j
��6��dgp��غ�ym�D���2�F�Ȓ�R����77����2̸ނ2\WWt��P8a�]>3�?��G�yz'�V1�'ETs�o����v���O���a}V��6U�T���}P�%�rn��K����2sB���!|�Т�\G����6��������ZL�Tl?֎��D4�i&�K��Mx'�Cq	.A	���9� s!�k�Ȫ��.�S�P�fqEٲ�Nar��.3c�c�l���C�{�^3,5!��<F/��NkJŢ{qۣ�_~���/�wV:� Ds���TC�9)Xd#$�}�i�v6�L��X�fr�E��z�B����z^���a$�Ȳ	k|�͆_yD7��#t藫��@ט���m��U���C1�hz�0tȭ�˕(�X*���!T�[o�F�k�wU�~�7��>n�n/�^�����޽�?8 %S      
   #   x�+40.I�44�BNC3#cSK�=... _��            x������ � �            x������ � �      (      x������ � �         �  x��Y[r#��n���->@��DV0? NTW�\�<�d����cRwRն�ո�i���h��5E�.�M#�SN��>}�s���������|9}���d磺q9�mܶ�Q��c��}�{��'�n���l�r��krҬ;m�n�g���O���	��b�Hb%5{	Z�D�N�!����l�ǋ����v=��۟�d���U��(����3�%�� T�X���<�!�:�:���}��z9����|۞������5����㗟��#���E���>����j�-g�Q#O����3��Ss���h�/���Y���[���no��Eܧk�mɰ픵�i��f:�{��ΟZF�K"�P�8�u�-��ӯ���_U�������¨dʱ'7����E9�Rg�(��}�z�f��D1t=���^{��;�������xT����-��P(��]A��kt�e`B��֚z�������m�ݜ!:�r�&q���2mk�O<�����vD5h�����l�Sol��<���b�Ic��M��5�1�-ࣘ��bM�lO�q{�~�A��M�6r��@&���Y�ڷ_�?o��A#\m���P+�T�k5�����Ί{~�9c�s&�ꎹ��Ru,m�!���~��~V��BLh�O`�!Q�tK'��Nq������l��|�tF�f`)��(1b0�bNK����
�^-���m��mB�6,l/O�(�An����(�"E,
R���ɏU)�m�G9��OR5�������R�&�� C�CK,jP,��!Sq�suJ�5�0d���ov=Σ�'!�2
ќ�U����M����4�l�
s��c����w��B�[��%̞�k�'�|���r �$�գi�_<��A&�4�6ٞ~�H<�_��"f����TA�Q�S4�ҥ:̈(�]G	EWE�3+��SB���'�aD���&�EA �`!�`EH1 �S߮&�Ǉ�|,�x]��4�����>@���uX�9���a@K����E�d�EPi` �cS��	������A���8:&�
�8�.�3�4�v��[��ō�H4y�����@�i ��٤V.��ĪMN��/�0����z�z<���s���K�6a�&%�0�,D�Vʈ#��8C�L�-����>ؙԉ1�:=��;�,i��y�������%�܋���9K7k�P��=\�W�j��H������C=�C�4���4Ff�����=���К�N�Ǌ�T�IbC�Y��@��8�Ť���#����!L��t�W��~��M�C(a!]�=��� ����DR\B�P��	�<)N���C`@G5��H@��2V��m�lA�9��O\����N���� �Mh�^G�/���(�0��X�`l��py�O	��+�B��/P��, �b����JK!����pI�܎��_�h6�<,M5T�rO����#z��k���mH�y���w��]��L��.�����zGU��1E����0P��`i ��K�T�c�>��da�wa� (��^f�����
��p������0H��6�wL����ǩ��ct����C�.pN!��;N~Q����)�
N��B�Φ$�o-�!\0��hrhV�� b��	��@�� 	p�z��9��sZjEWΉ���b�Y_	�k���wC����4%_�'x��Ű(��:�GJ���4Q�hDp�/�)e3ZG�P���&2�H3$�N^�Pr\]a,D]T�O042����(d4Q�u���
�e�\�P�36DmqQn�-�А��!��.REF���z��\}�9aw�Сڹ�zw�Q,��r�hx���#�D�ɷ@@$�z*�n��l �\����50���{��a������kBx��3��ܦ���@���y���Șh�ɝ��t�����ܙaB)u� ;:*�0��!p"��pJ��38%(IV�^�ׂN�b����(�G�>d2��XD������{����,�pQ$B+k��@�����Rzl��p@b��W1�
�CRj�G��bbr�mq��T&���� _M�X�JK�!�1�Gd��!p�Ȇ�ǂx����lm����ø�@o>(j�����Ӵ�R���z��a��Ȼ������p8�{���         M
  x����nc����sdK���2y�Y$AV��GY2$��N�wO�{ �p~��0d�:?y�Ud�� �M^X���:Z����j�����������;�����[;<�=�^��<��r��K��w˻z��w�Ji��O;�N<�t�o�h�/�:��ڞ����1�nH�*�KR�N^dߔ�=�������N�s{83�o�:����r��ُ��`�_�����Ǫ��U+��D�&	_IE"��-_�(fT��lU�❰���c�	N�e�S�!|O��/��z�WO��?�ĝ^v�o�˼^/d��Zȭz��(�$a�i";�o���;�#)�Dh��Xi��Ji(�_v_��Fh]�22�����h���l]QK�I�!�eW뾍�ƭ������w8}#5Ď���%���DV�Ρ�
Gx���1��f�����a<`������q��ב�*�XZM>�ޕVq�����<�Ef����h_ȑʸ��88P�_O��ө��ߝ��b�w�V�Gu�/[L�F'��=Ii��f�7����}w���Wr��@�<����KS�����%���������894)L4���]���n4�M�4����wy|�1č5�Y��<=��8�"%~�W�r��j�e��ϻˍƸ�u.�����Jc��Ҹj:�D���쌈�IA^����9��� ��j;�J�Z����R�6�qס�nĲ�y��^P�QxJ��<0�Y4F�;]~�t��q��x��x|���!7t�JW�se4�t3��1s	it	)Ϣ���xhy#3l{-3�d���k�p#��TS�<ۻ���DWNPB��7�s��r>>0�-�39erw8|��!�X�U:gQ�P����R��D�ӓT��(�o�ˋW�|Y��_��������.�BC���T*���:��Pd�<���z͵���^h��6�G"��^�P�O��&8��(�lQ�(��>:�
?������}��ΰk�ap�3�����_���x�7:��Q�\�'�\��r�p�HtYo��tE�ė��~�6N9�.�o�Q����z<_h�\�u���59zD�����:>}|z���G<�/��am���;L�0�}��]nf���֒Ldx!V�[�-��/ZhE��Z%��!��W�S~y{ɷ�n~%>�ý~>|�}�C�F�9O�MĕY-"�(ZQy�;)=Kc�J�؉���hap����}B�Q6B���DN������s=�V�%(��k��m�}��L��Ǜ��6��D�S����w�ď|�t���qdR�13UC�8�I�7�)�#�S��BA�@(��(
"𩝏o��~h������.ϻ��[�!7��RR�.do�a��"��E�؝�%�6�s@��p����Gw�˯�O��q����Ѷ����Ud�Q��Q�����feB�`e����X08:�B�/����ea˶�ܒJ��M%rj�W�W�K�C��}1�c��QCr� +��.ʊ,�NF��y}��T���l����oR��߽s�oK��R��q��%ȱ�V |����zb��'��c�؍��R�jY\�V�Ե��ẗ�(���nn|��7��������� t#�H�|yeaG���%���Q�Yfא��ǀ[_�e�e���!v,�"���$Z1�_j"��y�t�D����H��c�m��������|�2n��F�9�s�Y���]�R�V�EWRw��·27�����3 ���!�z7�M9���勜cU�ʳ�b��5@�Fnz%7���+�!n��5%p@7B+ͨ^�EM���(u/�65�!|��7�>ׁ�ѹ�=_� �X�L>�TR����JNdC�<��ը\��?A�Fnz�14���+�!n�1��u<3���L�g�_�\�/��,��qny%1$���+�!l�:'�2d��F.��r1a#�����ivn�}��uM�����|���Aȭ]ϒ��-���s��P�� /����E��c�m�l{(8���q�Ӿ�Mֽr"�y��ԅu�	�7@�#���y�fM�>�c�-��ap�`}��׽���-���{�ת���D[�R[��P���=A�fn|�.��(]���U�nh�hIx��k�E�z�!���<k����>���0G�������Z�V�_R���˵ CU�Z�K�JE�§}��G����7.�,�X�G��Zj��K2*�Z*g��c����>h��kr"���>܁��N�pӣc5��>��;��/�q�c,�%�p5�����'@���6S���E��1 �F��yA�ꗫ&W]�qj#�TL��p7օ�
��Y4�Fmݩ��lZ��K�N�h�r��Y�O}p	
Q�KP��(�G�y4��-����ȕ��i�z��T)�uF5���#���6�� |_4�Ʃ�4��۫*UX���51J-6��N73�:�m���`pt�����b6:�x�����/ˢ��YMhAx��2�ܢ7�+�+��n���Q���1@�F�o��'�bkemZ��"S.�ST.�f�7>.�q���5����u�����������m�z      �   �  x�}��n]!�g�=f��JU��C�,`�v�*�}���&j�F�����<�/�)qN5���p��]�������b�k�������͗��*��u]B��`�PhZhA$�8]��y=���	#~�tw�������W2��UY���A)�q)���b��`�7h�F��1�b���j�,�
V�f�A=,���v#ƕ�K����@hf(�C�C#^B��7�ʆ�9r"407���9�r�Dc<��c2M�ȍZ�¤0ܹ�$�y�p�|�����i<�~�r���u�������lb'/*{�x�F
�C(��K����;6���i��b@&�h,�ǫJnk�<�J	���z8�)�a/�V�\�C6���Na�Pz�A��=��[�@]D��F�	*yK���,��,�e���(��4e�,��Vf���۰Z��:4��%F�e1��0���y�t:�� ;�a      �     x��V[S�0~.����'D���a\fQY�^v�I�SȒ&�$U����K/H����r._Μ'O��i
�uB�0 �4V�$C�MFIeO'$ڀ�<m��7K�8��12L
��+e$=i��L)�
�)f�n��8���l��8A*RӊV�5��U]~!��6R����5Kh�q��o�*i��9�V5�>\y\ҙ���bԌ:��:J��kdW��E�^���LP���ޭ	��W��������|' \g�@�D`��M�o��c�K�����z=0��8�3:�	E�e{��}A��jo���7S��{�9[���md~ܐ1a}~�<��2���&��}���8I���7H(�d���	����s�r���ьָ�Ț��9�n눈�5�}���>E��ZLg���|O�@��
����[��E��y!q�٢p\��V �V���ԙePQ��"�X<��(`�pF���#G��Rh��W.ɏ��y���P��%���MV�M����3J�D��${2��	^'6S���QW����η�]6�`i:|"q�LC�tݳ���}߱�v���I�����$�� �(7Ueì�*���(�v�$���){�G��Q0�|d��ݓ�6�'(�E+�w\}� ����n$�,��b�J��`������qh�H��A냺]x��%��.(�|�(�v�";�r�Z�5%��*�l�i�����٩������nҶݽ3��-x;�۵��iY�r�?'�J�?Z�V�             x������ � �      �   �   x�Mλ1���I�O��D���.��?̐�o��?_��,�}���n�&���ؼ�r��pױÄ�VD5f���������g�fƔ�	m��U��4zkJ<l*qV�N��W��f���Q��R���3�      	      x������ � �      �   -   x��M,.I-��J�/.���OO��K���+.-J�KNE����� ��m      �   /   x�+H,..�/J�,�1J�07��$��� ��g^qiQb^r*W� 3��      �   l   x�e��
�0g�cB[��5��E$N���'���PL��q�T�\�H��U�8��~bE"�x��"�E����i7���4ۛ�����Sm�Z����O�!9�f>�VC�            x������ � �      �   ;  x�}�InAE���j�t�!�EZFz�-��)P�M�����)p�57H��'��%B��&��1ē���n��d��a~L1�f�eH�
�z lB�i"���MKIK����FT<��"ݖ���`_>��ۺo�8U����aFu��	�\��$����qǕ�/�^S��E��V�=Z���.b.Z��<QD�z�,��Qv��6v�1�{�����qRȡ���0&���A}����q�/�<����m?\ϼ-f��q��q�	�הJ�.��F,#�E���¬�:u��7��m`�!v�����y���"���            x������ � �         '  x����jA��w��/���ќ.C�CZ�N)�fFS�]|���;n��N����_�oKMA!y ��(C*`�
!����tt�y1�p1���d��?�^�w��ɦ�u�4�bo��u�y�@:D�0ۊ��5v����l��L�ף�a7�Vx'��f]��r\�Y a��)֧F��FCL6��ֻ����r��z�~>.f�٤�\����~#���Q�gq
{J�L
�)H�֞����k���߮.no?��W�����zS��'Դ���۲I5��T2�X�X�Z��v�����UY��ն��A�O�Ym��$����*�l'��9Z��m�����!��l�����s͛4��-�v&A!b߮�\Kl�\r�ٵe:��y[�W���������JH���z�Xo��<��I�o�uj�/a�YĬTp�0*��r��!�o�넝:���c�Y@�L�G�:�_4�B��7!;?�g��X�dg���,x�����Ȝ����5Ԙ�F$g#Y� Ơ����Ɇ"�?Q;�%ھ����'x2pA      >      x������ � �      *      x������ � �      )      x������ � �      $      x������ � �      =      x������ � �      '      x������ � �      %      x������ � �      &      x������ � �      ?      x������ � �      @      x������ � �      �   �   x����� C��^���(���/a��13��<�kT�����pˀ�Z�6�d����`ņ9�jwO�4��\!8����A�u���1�+����z�G�:/�5%�̂����d��5nv���$Yl�I1!���^�cS7�ߑv��I!����8�F(����H)o��#�H>�H��N��������Z�ۢ`�      +      x������ � �      �      x������ � �            x������ � �      ;      x������ � �      �   �  x���=n�0�k�)�2(���R�I�`�TiD����<N��؋�&v`��I"���Q'b�"�IJA���DJc����O]w<�x<���}i���i
��)^G捴���&Ho��vbQ9�DE �(|��5i�>�¥��t����q�����u�}���w���0�r
���:E�(�Ez��,@���t�-q���p%�Cz�\rˁ�"1HVI9�K�̮�k6(�� �&#�-�U�Ui�x8u�x�Gl38c.�M�ͣ78�Be&8�B.�iꜽ��N���^�C���ɗ��W�M�oR3�{Ŋg>����p��?v���G^��oD�k���:�'�c�2�(�C�op"��CH'��Q��IrQlO����/����[��2��n��r��o��R��Vk����^��n�b	L�      �      x������ � �            x������ � �            x������ � �      �      x������ � �            x������ � �      �      x������ � �      �   �  x�����8 ��sa ��K_��C�������LGL�`3M��7���M��G�	�@��f Nf������ܮFi�agÝ� o<�E���#Փ!kaTN�pf>؊�X��I�4T	P�d�Ca�c�ڕ*�Hg��1��H��mY�D�[כ$�|#���Gz�q�p���Dk���&?��X"�+�
7l�:��?>ztZ@�G���g���"�~�{$|��^�旓�^�v��.�#�~U�p�R[]������$r��Gjy��qa��+W�� �<J�\��<�H-O-R�S���D�"v�FLIW��&�J�|�g�S����"�<�H!�*A�~���Y�p�%2�o�˞�CH��>�@Vh�y���3���n��VNw��+$L_�Ce꺮�`���"����4����`��I[Ơ��4��EjuA����U��e|��G��y*���)z5���!$�=�-R�x��2�"���H���~��
D�      �      x������ � �            x������ � �      �      x������ � �      �   w   x��;�0 Й\�r�'������U�����6d*��\,���yy��J2��W�@�T;Bǹ4|Zcvl0I�i� 1*���t��������L����;����R���&y     