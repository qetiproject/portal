/**
 * Demo API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { Recipient } from './recipient';
import { TimeOffRequestType } from './timeOffRequestType';

export interface TimeoffrequestCreatetimeoffrequestBody { 
    type?: TimeOffRequestType;
    title?: string;
    recipient?: Recipient;
    receiver?: string;
    deadLine?: string;
    from?: string;
    including?: string;
    file?: Blob;
    description?: string;
}