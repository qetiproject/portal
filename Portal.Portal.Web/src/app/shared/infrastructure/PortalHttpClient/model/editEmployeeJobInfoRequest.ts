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
import { TimeZoneEnum } from './timeZoneEnum';

export interface EditEmployeeJobInfoRequest { 
    id?: number;
    region?: string;
    workAddress?: string;
    idExpirationDate?: Date;
    department?: string;
    timeZone?: TimeZoneEnum;
}