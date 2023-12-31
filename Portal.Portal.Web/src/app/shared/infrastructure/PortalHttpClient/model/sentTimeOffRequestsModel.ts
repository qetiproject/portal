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
import { ContentModel } from './contentModel';
import { TimeOffRequestStatus } from './timeOffRequestStatus';

export interface SentTimeOffRequestsModel { 
    number?: string;
    status?: TimeOffRequestStatus;
    date?: Date;
    receiver?: string;
    title?: string;
    content?: ContentModel;
}